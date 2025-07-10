using System.Threading.Tasks;
using Godot;
using SpireQuiz.Scripts.TopBar;

namespace SpireQuiz.Scripts.Sections.FillTheCategory;

public partial class SectionFillTheCategory: Section
{
	public override SectionType SectionType { get; set; } = SectionType.FillTheCategory;
	protected override string Rules => "Team members take turns guessing an item which fits into the displayed category by typing it in the Discord Chat!\n" +
	                                   $"At the end, correct guesses give {CORRECT_POINTS} points, incorrect ones give {INCORRECT_POINTS}\n" +
	                                   "The team has 3 minutes.";

	#region Constants

	private int CORRECT_POINTS = 1;
	private int INCORRECT_POINTS = -1;

	#endregion
	
	#region Exports
	
	[Export] private TextEdit AnswerTextEdit;
	[Export] private RichTextLabel ResultLabel;

	#endregion
	
	private FillTheCategoryQuestion CurrentQuestion;
	
	public override async Task Begin()
	{
		Visible = true;
		RulesLabel.Text = Rules;
		StartButton.Visible = true;
	}
	
	public async void StartButtonPressed()
	{
		StartButton.Visible = false;
		QuestionIndex = -1;
		CurrentQuestion = null;
		await TryLoadNextQuestion();
	}

	public async void NextQButtonPressed()
	{
		await TryLoadNextQuestion();
	}

	public async void ValidateButtonPressed()
	{
		var correctAnswers = CurrentQuestion.Answers;
		for (int i = 0; i < correctAnswers.Count; i++)
		{
			correctAnswers[i] = FormatAnswer(correctAnswers[i]);
		}

		int correct = 0;
		int incorrect = 0;

		ResultLabel.Text = $"[color=#00ff00]Correct: {correct * CORRECT_POINTS}[/color] [color=#ff0000]Incorrect: {incorrect * INCORRECT_POINTS}[/color]";
	}

	private async Task TryLoadNextQuestion()
	{
		QuestionIndex++;
		if (QuestionIndex >= FillTheCategoryQuestion.AllQuestions.Count)
		{
			await End();
			return;
		}
		CurrentQuestion = FillTheCategoryQuestion.AllQuestions[QuestionIndex];
		QuestionLabel.Text = CurrentQuestion.Text;
		ResultLabel.Text = "";
		AnswerTextEdit.Text = "";

		NowGuessing = QuestionIndex % 2 == 0 ? TeamColor.Blue : TeamColor.Red;
		
		GameTimer.Instance.SetTime(180);
		GameTimer.Instance.Start();
	}

	private string FormatAnswer(string a)
	{
		a = a.ToLower();
		a = a.Replace(".", "");
		a = a.Replace("'", "");
		return a;
	}
	
}