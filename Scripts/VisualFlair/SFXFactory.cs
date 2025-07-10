using System.Collections.Generic;
using System.IO;
using Godot;
using SpireKnight.Scripts.Helpers;
using SpireKnight.Scripts.Helpers.Animations;
using SpireKnight.Scripts.Helpers.Animations.Instances;
using SpireKnight.Scripts.VisualFlair.Instances.PlonkText;

namespace SpireKnight.Scripts.VisualFlair;

public partial class SFXFactory : Node2D
{
	public static SFXFactory Instance;

	[Export] private PackedScene PlonkText;
	
	public bool IsPlonkTextEnabled;
	
	public override void _Ready()
	{
		Instance = this;
		Initialize();
	}

	private void Initialize()
	{
	}
	
	public PlonkTextController CreatePlonkText(string text, Vector2 where, Color? color = null)
	{
		
		var sfx = (PlonkTextController)PlonkText.Instantiate();
		AddChild(sfx);
		sfx.GlobalPosition = where;
		sfx.ZAsRelative = false;
		sfx.ZIndex = 1000;

		if (color != null)
		{
			text = text.RichWrapColor((Color)color);
		}

		sfx.Label.Text = text;

		sfx.Label.Modulate = new Color(1, 1, 1, 0);
		var anim = new EAnimModulate(sfx.Label, new Color(1, 1, 1, 1), 0.3f);
		EAController.Instance.ExclusiveAnimate(anim);
		
		return sfx;
	}
	




}