using Godot;

namespace SpireKnight.Scripts.Helpers.Animations.Mods;

public class ExclusiveAnimationModArcH: ExclusiveAnimationMod
{
	private float Offset;

	public ExclusiveAnimationModArcH(float offset = 130)
	{
		Offset = offset;
	}

	public override void Process(Node2D node, double at)
	{
		node.Position += Vector2.Left * (float)Mathf.Sin(at * Mathf.Pi) * Offset;
	}

}