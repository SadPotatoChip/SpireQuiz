using Godot;
using Godot.Collections;

namespace SpireQuiz.Scripts;

public static class Teams
{
	public const string BlueTeamColor = "00ffff";
	public const string RedTeamColor = "ff0000";

	public static Dictionary<TeamColor, string> ColorForTeam = new ()
	{
		{ TeamColor.Blue, BlueTeamColor },
		{ TeamColor.Red, RedTeamColor },
	};
	
	public static TeamColor Other(TeamColor t)
	{
		return t == TeamColor.Blue ? TeamColor.Red : TeamColor.Blue;
	}
}

public enum TeamColor
{
	Blue = 0,
	Red = 1
}

