using System.Collections.Generic;

namespace SpireQuiz.Scripts.Sections.Trivia;

public class TriviaQuestion
{
	public string Text;
	public bool Answered;
	public int TimesAppeared;
	
	public TriviaQuestion(string text)
	{
		Text = text;
	}

	public static List<TriviaQuestion> AllQuestions => new()
	{ 
		"1.	What is the name of the Relic which appears in place of new Relics when the Relic pool is exhausted?",
		"2.	What is the name of the Curse which can be gained only as part of a Blight in Endless mode?",
		"3.	How many Stances does Watcher have?",
		"4.	What is the first name of either of the developers of Slay the Spire?"
	};
	
	public static implicit operator TriviaQuestion(string s) => new TriviaQuestion(s);
}