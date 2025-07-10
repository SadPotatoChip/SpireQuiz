using System.Threading.Tasks;
using Godot;
using SpireKnight.Scripts.VisualFlair;

namespace SpireQuiz.Scripts.TopBar;

public partial class GameInformation : Control
{
	public static GameInformation Instance;

	#region Exports

	[Export] private TextEdit BlueTeamScoreField;
	[Export] private TextEdit BlueTeamNameField;
	[Export] private TextEdit BlueTeamMembersField;
	[Export] private TextEdit RedTeamScoreField;
	[Export] private TextEdit RedTeamNameField;
	[Export] private TextEdit RedTeamMembersField;

	#endregion


	public override void _Ready()
	{
		Instance = this;
	}

	public async Task GiveScore(TeamColor color, int n)
	{
		var field = color == TeamColor.Blue ? BlueTeamScoreField : RedTeamScoreField;
		try
		{
			var current = int.Parse(field.Text);
			field.Text = (current + n).ToString();
			var plonk = SFXFactory.Instance.CreatePlonkText((n > 0 ? "+" : "") + n, field.GlobalPosition + Vector2.Right * 150, Colors.Yellow);
			plonk.FloatUp(50, 3f);
		}
		catch
		{
			GD.PrintErr($"Invalid text in score field {color}, failed to add " + n );
		}
	}
}