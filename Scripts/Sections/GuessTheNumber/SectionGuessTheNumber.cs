using System;
using System.Threading.Tasks;
using Godot;
using SpireQuiz.Scripts.Sections.FillTheCategory;
using SpireQuiz.Scripts.TopBar;

namespace SpireQuiz.Scripts.Sections.GuessTheNumber;

public partial class SectionGuessTheNumber: Section
{
	public override SectionType SectionType { get; set; } = SectionType.GuessTheNumber;
	protected override string Rules => "A question with a number as the answer appears!\n" +
	                                   "Teams have 2 minutes to go into separate voice channels and discuss their answer\n"+
	                                   $"Whoever is closer to the answer gets {CLOSER_POINTS} points, a correct answer grants an extra {CORRECT_POINTS} points";

	#region Constants

	private int CORRECT_POINTS = 3;
	private int CLOSER_POINTS = 6;

	#endregion
	
	#region Exports
	
	[Export] private TextEdit AnswerBlueTextEdit;
	[Export] private TextEdit AnswerRedTextEdit;
	[Export] private RichTextLabel ResultLabel;

	#endregion
	
	private GuessTheNumberQuestion CurrentQuestion;
	
	public override async Task Begin()
	{
		Visible = true;
		RulesLabel.Text = Rules;
		QuestionIndex = -1;
		CurrentQuestion = null;
	}

	public async void OnCalculateButtonPressed()
	{
		var blueAnswer = int.Parse(AnswerBlueTextEdit.Text);
		var redAnswer = int.Parse(AnswerRedTextEdit.Text);

		var blueDiff = Math.Abs(CurrentQuestion.Answer - blueAnswer);
		var redDiff = Math.Abs(CurrentQuestion.Answer - redAnswer);

		if (blueDiff == redDiff)
		{
			await GameInformation.Instance.GiveScore(TeamColor.Red, CLOSER_POINTS);
			await GameInformation.Instance.GiveScore(TeamColor.Red, CLOSER_POINTS);
		}
		else if (blueDiff < redDiff)
		{
			await GameInformation.Instance.GiveScore(TeamColor.Blue, CLOSER_POINTS);
		}
		else
		{
			await GameInformation.Instance.GiveScore(TeamColor.Red, CLOSER_POINTS);
		}

		await GameTimeFlow.Stop(500);
		if (blueDiff == 0)
		{
			await GameInformation.Instance.GiveScore(TeamColor.Blue, CORRECT_POINTS);
		}
		if (redDiff == 0)
		{
			await GameInformation.Instance.GiveScore(TeamColor.Red, CORRECT_POINTS);
		}
	}

	private async Task TryLoadNextQuestion()
	{
		QuestionIndex++;
		if (QuestionIndex >= GuessTheNumberQuestion.AllQuestions.Count)
		{
			await End();
			return;
		}
		CurrentQuestion = GuessTheNumberQuestion.AllQuestions[QuestionIndex];
		QuestionLabel.Text = CurrentQuestion.Text;
		ResultLabel.Text = "";

		//NowGuessing = QuestionIndex % 2 == 0 ? TeamColor.Blue : TeamColor.Red;
		
		GameTimer.Instance.SetTime(120);
		GameTimer.Instance.Start();
	}
}