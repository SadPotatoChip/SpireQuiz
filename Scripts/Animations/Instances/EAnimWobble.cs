using System;
using Godot;

namespace SpireKnight.Scripts.Helpers.Animations.Instances;

public class EAnimWobble: ExclusiveAnimation
{
	private Vector2 OriginalScale;
	private Control TargetControl;
	private float Magnitude;
	
	private Vector2 StartScale;

	public Func<double, double> EasingFunction = easeOutElastic;
	
	public EAnimWobble(Control node, float magnitude, double duration = 1f, ExclusiveAnimationPriority priority = ExclusiveAnimationPriority.Normal)
	{
		Duration = duration;
		base.Target = TargetControl = node;
		Priority = priority;
		Magnitude = magnitude;
	}
	
	public override void Start()
	{
		OriginalScale = TargetControl.Scale;
		
		Magnitude = Math.Min(1, Magnitude);
		
		StartScale = TargetControl.Scale * (1 + Magnitude);
		TargetControl.Scale = StartScale;
	}

	protected override void AfterFinished()
	{
		TargetControl.Scale = OriginalScale;
	}

	public override void Process(double delta)
	{
		Time += delta;
		var percentage = Time / Duration;

		var at = EasingFunction(percentage);
		TargetControl.Scale = Lerp(StartScale, OriginalScale, at);
	}

	public override void Stop()
	{
		base.Stop();
		TargetControl.Scale = OriginalScale;
	}
}