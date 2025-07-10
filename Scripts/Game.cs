using Godot;
using SpireQuiz.Scripts.Sections;

namespace SpireQuiz.Scripts;

public partial class Game : Node2D
{
	public static Game Instance;

	[Export] public SectionPicker SectionPicker;
	
	public override void _Ready()
	{
		Instance = this;
		SectionPicker.Visible = true;
	}
}