using System;
using System.Linq;
using Godot;
using SpireKnight.Scripts.Helpers.Animations.Mods;

namespace SpireKnight.Scripts.Helpers.Animations.Instances;

public class EAnimTransform : ExclusiveAnimation
{
	private readonly Vector2 TargetPosition;
	private readonly Vector2 TargetScale;
	private readonly double TargetRotation;

	private readonly bool UsePosition;
	private readonly bool UseScale;
	private readonly bool UseRotation;
	
	private Vector2 StartPosition;
	private Vector2 StartScale;
	private double StartRotation;
	private readonly Node2D Target2D;

	public Func<double, double> EasingFunction;

	public EAnimTransform(Node2D node, double duration, Vector2? targetPosition = null, Vector2? targetScale = null,
		double? targetRotation = null, double initialDelay = 0, ExclusiveAnimationPriority priority = ExclusiveAnimationPriority.Normal, params ExclusiveAnimationMod[] mods)
	{
		Target = Target2D = node;
		if (targetPosition != null)
		{
			TargetPosition = (Vector2)targetPosition;
			UsePosition = true;
		}
		if (targetScale != null)
		{
			TargetScale = (Vector2)targetScale;
			UseScale = true;
		}

		if (targetRotation != null)
		{
			TargetRotation = (double)targetRotation;
			UseRotation = true;
		}

		Priority = priority;
		InitialDelay = initialDelay;
		Duration = duration;
		Time = 0;

		Mods = mods?.ToList();

		EasingFunction = easeOutCubic;
	}

	public override void Start()
	{
		StartPosition = Target2D.Position;
		StartScale = Target2D.Scale;
		StartRotation = Target2D.Rotation;
	}

	protected override void AfterFinished()
	{
		
	}

	public override void Process(double deltaTime)
	{
		Time += deltaTime;
		var percentage = Math.Max(0, Time - InitialDelay) / Duration;
		var at = EasingFunction(percentage);

		if (UsePosition)
		{
			Target2D.Position = Lerp(StartPosition, TargetPosition, at);
		}
		if (UseScale)
		{
			Target2D.Scale = Lerp(StartScale, TargetScale, at);
		}
		if (UseRotation)
		{
			Target2D.Rotation = (float)Lerp(StartRotation, TargetRotation, at);
		}

		if (Mods != null)
		{
			foreach (var mod in Mods)
			{
				mod.Process(Target2D, at);
			}
		}
	}
	
}