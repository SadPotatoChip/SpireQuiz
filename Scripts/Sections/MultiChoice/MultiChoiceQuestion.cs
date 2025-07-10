using System.Collections.Generic;
using System.Linq;

namespace SpireQuiz.Scripts.Sections.MultiChoice;

public class MultiChoiceQuestion
{
	public string Text;
	public List<string> Answers;
	public int CorrectAnswer;

	public MultiChoiceQuestion(string text, int correctAnswer, params string[] answers)
	{
		Text = text;
		Answers = answers.ToList();
		CorrectAnswer = correctAnswer;
	}

	public static List<MultiChoiceQuestion> AllQuestions = new()
	{
		new MultiChoiceQuestion(
			"Which of the following is NOT a quote from the Merchant?",
			3,
			"I used to be like you", "Stay a while and listen", 
			"You look dangerous, hehe…", "My favorite customer!"
		),
		new MultiChoiceQuestion(
			"When was Slay the Spire first released to Early Access?",
			1,
			"2016", "2017", "2018", "2019"
		),
		new MultiChoiceQuestion(
			"Which of the following is NOT a Rare relic",
			1,
			"Turnip", "Meat on the Bone", "Du-Vu Doll", "Bird-Faced Urn"
		),
		new MultiChoiceQuestion(
			"Which of the following Ascension modifier appears above Ascension 10?",
			1,
			"Bosses deal more damage with their attacks.", "Upgraded cards appear less often.", 
			"Heal less after Boss battles.", "Elites spawn more often."
		),
		new MultiChoiceQuestion(
			"Which Character has most the fewest Powers in their card pool?",
			1,
			"Ironclad", "Silent", "Defect", "Watcher"
		),
		new MultiChoiceQuestion(
			"What is the name of the Game Engine used to develop Slay the Spire?",
			0,
			"libGDX", "Godot", "RayLib", "mini2Dx"
		),
	};
}

