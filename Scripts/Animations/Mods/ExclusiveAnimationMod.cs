using Godot;

namespace SpireKnight.Scripts.Helpers.Animations.Mods;

public abstract class ExclusiveAnimationMod
{
	public abstract void Process(Node2D node, double at);
}