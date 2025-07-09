using Godot;

namespace SpireKnight.Scripts.Helpers.Animations.Mods;

public class ExclusiveAnimationModArc : ExclusiveAnimationMod
{
	private float Height;

	public ExclusiveAnimationModArc(float height = 130)
	{
		Height = height;
	}

	public override void Process(Node2D node, double at)
	{
		node.Position += Vector2.Up * (float)Mathf.Sin(at * Mathf.Pi) * Height;
	}
}