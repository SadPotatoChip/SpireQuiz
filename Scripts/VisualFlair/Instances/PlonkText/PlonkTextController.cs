using Godot;
using SpireKnight.Scripts.Helpers.Animations;
using SpireKnight.Scripts.Helpers.Animations.Instances;

namespace SpireKnight.Scripts.VisualFlair.Instances.PlonkText;

public partial class PlonkTextController : SFXController
{
	[Export] public RichTextLabel Label;
	[Export] private RigidBody2D RB;
	
	public void FloatUp(int distanceUp = 200, float duration = 1.5f)
	{
		var floatAnim = new EAnimTransform(this, duration,
			Position + Vector2.Up * distanceUp, Vector2.One * 1.2f);
		var fadeAnim = new EAnimModulate(this, new Color(1, 1, 1, 0), 0.5);
		floatAnim.OnFinished += () => EAController.Instance.ExclusiveAnimate(fadeAnim);
		EAController.Instance.ExclusiveAnimate(floatAnim);
	}
}