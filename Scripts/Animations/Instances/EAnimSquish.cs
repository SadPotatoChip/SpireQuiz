using System;
using Godot;

namespace SpireKnight.Scripts.Helpers.Animations.Instances;

public class EAnimSquish : ExclusiveAnimation
{
	private Vector2 OriginalScale;
	private readonly Node2D Target2D;
	private readonly float Squish;

	public Func<double, double> EasingFunction = easeOutElastic;

	
	public EAnimSquish(Node2D node, float magnitude, double duration = 1f, ExclusiveAnimationPriority priority = ExclusiveAnimationPriority.Normal)
	{
		Duration = duration;
		Target = Target2D = node;
		Priority = priority;
		Squish = Math.Max(0.1f, 1 - magnitude);
	}

	public override void Start()
	{
		OriginalScale = Target2D.Scale;
		Target2D.Scale = VolumeConstantScale(OriginalScale, Squish);
	}

	protected override void AfterFinished()
	{
		Time = Duration;
		Target2D.Scale = OriginalScale;
	}

	public override void Process(double delta)
	{
		Time += delta;
		var percentage = Time / Duration;
		var at = EasingFunction(percentage);

		var lerp = Lerp(Squish, 1f, (float)at);
		Target2D.Scale =  VolumeConstantScale(OriginalScale, lerp);
		
		if (Mods != null)
		{
			foreach (var mod in Mods)
			{
				mod.Process(Target2D, at);
			}
		}
	}

	public override void Stop()
	{
		base.Stop();
		Target2D.Scale = OriginalScale;
	}

	private Vector2 VolumeConstantScale(Vector2 originalScale, float squish)
	{
		return new Vector2(originalScale.X / squish, originalScale.Y * squish);
	}
}