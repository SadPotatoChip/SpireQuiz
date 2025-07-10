using System.Collections.Generic;

namespace SpireQuiz.Scripts.Sections.GuessTheNumber;

public class GuessTheNumberQuestion
{
	public string Text;
	public int Answer;

	public GuessTheNumberQuestion(string text, int answer)
	{
		Text = text;
		Answer = answer;
	}

	public static List<GuessTheNumberQuestion> AllQuestions = new()
	{
		new ("What is the highest possible cost of a Relic at the Shop? (Excluding alternative game modes)", 346),
		new ("How many different potions are there in the game?", 42),
		new ("Excluding Terror, what is the highest number that appears on an unupgraded card?", 50),
		new ("During Early Access, the developers put out weekly patches to the game. How many weekly patches were there in total?", 56),

	};
}