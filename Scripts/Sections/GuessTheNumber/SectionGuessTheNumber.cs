using System;
using System.Threading.Tasks;
using Godot;
using SpireKnight.Scripts.Audio;
using SpireKnight.Scripts.VisualFlair;
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
	[Export] private SoundQueue FinishAniticpationSound;
	[Export] private SoundQueue StartAniticpationSound;

	#endregion
	
	private GuessTheNumberQuestion CurrentQuestion;
	
	public override async Task Begin()
	{
		Visible = true;
		RulesLabel.Text = Rules;
		QuestionIndex = -1;
		CurrentQuestion = null;
	}

	public async void OnSubmitButtonPressed()
	{
		GameTimer.Instance.Stop();
		var lerp = 0.0033f;
		var limit = (int)(1 / lerp);
		StartAniticpationSound.PlaySound();
		for (int percent = 0; percent < limit; percent++)
		{
			var floatPercent = percent * lerp;
			var at = easeOutCubic(floatPercent);
			ResultLabel.Text = ((int)(at * CurrentQuestion.Answer)).ToString();
			await GameTimeFlow.Stop(5);
		}
		ResultLabel.Text = CurrentQuestion.Answer.ToString();
		FinishAniticpationSound.PlaySound();
		
		var blueAnswer = int.Parse(AnswerBlueTextEdit.Text);
		var redAnswer = int.Parse(AnswerRedTextEdit.Text);

		var blueDiff = Math.Abs(CurrentQuestion.Answer - blueAnswer);
		var redDiff = Math.Abs(CurrentQuestion.Answer - redAnswer);

		if (blueDiff == redDiff)
		{
			await GameInformation.Instance.GiveScore(TeamColor.Red, CLOSER_POINTS);
			await GameInformation.Instance.GiveScore(TeamColor.Blue, CLOSER_POINTS);
			var plonk = SFXFactory.Instance.CreatePlonkText("Draw!", AnswerBlueTextEdit.GlobalPosition, Colors.Yellow);
			plonk.FloatUp();
			var plonk2 = SFXFactory.Instance.CreatePlonkText("Draw!", AnswerRedTextEdit.GlobalPosition, Colors.Yellow);
			plonk2.FloatUp();
		}
		else if (blueDiff < redDiff)
		{
			await GameInformation.Instance.GiveScore(TeamColor.Blue, CLOSER_POINTS);
			var plonk = SFXFactory.Instance.CreatePlonkText("WIN!", AnswerBlueTextEdit.GlobalPosition, Colors.Green);
			plonk.FloatUp();
		}
		else
		{
			await GameInformation.Instance.GiveScore(TeamColor.Red, CLOSER_POINTS);
			var plonk = SFXFactory.Instance.CreatePlonkText("WIN!", AnswerRedTextEdit.GlobalPosition, Colors.Green);
			plonk.FloatUp();
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

	public async void OnNextQuestionButtonPressed()
	{
		await TryLoadNextQuestion();
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
		AnswerBlueTextEdit.Text = "";
		AnswerRedTextEdit.Text = "";

		//NowGuessing = QuestionIndex % 2 == 0 ? TeamColor.Blue : TeamColor.Red;
		
		GameTimer.Instance.SetTime(120);
		GameTimer.Instance.Start();
	}
	
	public static double easeOutCubic(double x) {
		return 1 - Math.Pow(1 - x, 3);
	}
}