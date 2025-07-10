using System.Linq;
using System.Threading.Tasks;
using Godot;
using SpireKnight.Scripts.VisualFlair;
using SpireQuiz.Scripts.TopBar;

namespace SpireQuiz.Scripts.Sections.FillTheCategory;

public partial class SectionFillTheCategory: Section
{
	public override SectionType SectionType { get; set; } = SectionType.FillTheCategory;
	protected override string Rules => "Team members take turns guessing an item which fits into the displayed category!\n" +
	                                   $"At the end, correct guesses give {CORRECT_POINTS} points, incorrect ones give {INCORRECT_POINTS}\n" +
	                                   "The team has 3 minutes.";

	#region Constants

	private int CORRECT_POINTS = 2;
	private int INCORRECT_POINTS = -1;

	#endregion
	
	#region Exports
	
	[Export] private TextEdit AnswerTextEdit;
	[Export] private RichTextLabel ResultLabel;
	[Export] private RichTextLabel OutputLabel;

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

		var answers = AnswerTextEdit.Text.Split(",")
			.Where( s=> false == string.IsNullOrWhiteSpace(s));
		var outputText = "";
		foreach (var a in answers)
		{
			var s = a;
			if (correctAnswers.Contains(FormatAnswer(a)))
			{
				correct++;
				s = s.RichWrapColor(Colors.Green);
			}
			else
			{
				incorrect++;
				s = s.RichWrapColor(Colors.Red);
			}
			outputText += s + ", ";
		}
		OutputLabel.Text = outputText;
		ResultLabel.Text = $"[color=#00ff00]Correct: {correct * CORRECT_POINTS}[/color] [color=#ff0000]Incorrect: {incorrect * INCORRECT_POINTS}[/color]";

		await GameInformation.Instance.GiveScore(NowGuessing, correct * CORRECT_POINTS + incorrect * INCORRECT_POINTS);
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
		OutputLabel.Text = "";

		NowGuessing = QuestionIndex % 2 == 0 ? TeamColor.Blue : TeamColor.Red;
		
		GameTimer.Instance.SetTime(180);
		GameTimer.Instance.Start();
	}

	private string FormatAnswer(string a)
	{
		a = a.ToLower();
		a = a.Replace(".", "");
		a = a.Replace("'", "");
		a = a.Replace(" ", "");
		return a;
	}
	
}