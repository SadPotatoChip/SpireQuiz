using System;
using Godot;

namespace SpireKnight.Scripts.Helpers.Animations.Instances;

public class EAnimModulate: ExclusiveAnimation
{
	private Color OriginalColor;
	private readonly Color TargetColor;
	private readonly CanvasItem TargetSprite;
	private readonly bool SetVisible;
	private readonly double InitialDelay;
	
	public EAnimModulate(CanvasItem node, Color targetColor, double duration = 1, float initialDelay = 0)
	{
		Target = TargetSprite = node;
		Duration = duration;
		TargetColor = targetColor;
		InitialDelay = initialDelay;
	}

	public override void Start()
	{
		OriginalColor = TargetSprite.Modulate;
	}

	public override void Process(double delta)
	{
		Time += delta;
		
		var percentage = Math.Max(0, Time - InitialDelay) / Duration;
		percentage = easeOutCubic(percentage);
		TargetSprite.Modulate = Lerp(OriginalColor, TargetColor, (float)percentage);
	}

	protected override void AfterFinished()
	{
		TargetSprite.Modulate = TargetColor;
	}
}