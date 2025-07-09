using Godot;

namespace SpireKnight.Scripts.Helpers.Animations.Mods;

public class ExclusiveAnimationFullArc: ExclusiveAnimationMod
{
	private float Height;

	public ExclusiveAnimationFullArc(float height = 130)
	{
		Height = height;
	}

	public override void Process(Node2D node, double at)
	{
		node.Position += Vector2.Up * (float)Mathf.Sin(at * 2 * Mathf.Pi) * Height;
	}
}