using System;
using System.Threading.Tasks;
using Godot;
using SpireKnight.Scripts.Audio;
using SpireKnight.Scripts.VisualFlair;
using SpireQuiz.Scripts.TopBar;

namespace SpireQuiz.Scripts.Sections.MultiChoice;

public partial class SectionMultiChoice : Section
{
	public override SectionType SectionType { get; set; } = SectionType.MultiChoiceQuestions;


	[Export] private TextureButton[] AnswerButtons;
	[Export] private SoundQueue DrumRollSound;
	[Export] private SoundQueue CorrectSound;
	[Export] private SoundQueue IncorrectSound;
	
	protected override string Rules =>
		"Teams take turns answering questions. You have 2 minutes to discuss before giving a final answer.\n" +
		$"A correct answer gives {POINT_REWARD} points.\n" +
		$"An incorrect answer gives the other team a chance to answer (1 minute). Correct answer gives them {POINT_REWARD_OTHER} points!";

	private const int POINT_REWARD = 8;
	private const int POINT_REWARD_OTHER = 4;
	private int CurrentReward;

	private MultiChoiceQuestion CurrentQuestion;
	
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

	private async Task TryLoadNextQuestion()
	{
		QuestionIndex++;
		if (QuestionIndex >= MultiChoiceQuestion.AllQuestions.Count)
		{
			await End();
			return;
		}

		NowGuessing = QuestionIndex % 2 == 0 ? TeamColor.Blue : TeamColor.Red;
		CurrentReward = POINT_REWARD;
		CurrentQuestion = MultiChoiceQuestion.AllQuestions[QuestionIndex];
		QuestionLabel.Text = CurrentQuestion.Text;
		for (int i = 0; i < CurrentQuestion.Answers.Count; i++)
		{
			AnswerButtons[i].SelfModulate = new Color(1, 1, 1);
			AnswerButtons[i].GetChild<RichTextLabel>(1).Text = CurrentQuestion.Answers[i];
		}
		GameTimer.Instance.SetTime(120);
		GameTimer.Instance.Start();
	}

	public async void AnswerPressed(int index)
	{
		await GameTimeFlow.Stop(200);
		DrumRollSound.PlaySound();
		var correct = index == CurrentQuestion.CorrectAnswer;
		if (correct)
		{
			await CorrectAnswer(AnswerButtons[index]);
		}
		else
		{
			await WrongAnswer(AnswerButtons[index]);
		}
	}

	private async Task CorrectAnswer(TextureButton btn)
	{
		await GameTimeFlow.Stop(2000);
		CorrectSound.PlaySound();
		btn.SelfModulate = new Color(0, 1, 0);
		var plonk = SFXFactory.Instance.CreatePlonkText("Correct!", btn.GlobalPosition + Vector2.Up*50 + Vector2.Right * 450, Colors.Green);
		plonk.FloatUp();
		await GameInformation.Instance.GiveScore(NowGuessing, CurrentReward);
		
		await GameTimeFlow.Stop(2000);
		await TryLoadNextQuestion();
	}
	
	private async Task WrongAnswer(TextureButton btn)
	{
		await GameTimeFlow.Stop(2000);
		IncorrectSound.PlaySound();
		btn.SelfModulate = new Color(1, 0, 0);
		var plonk = SFXFactory.Instance.CreatePlonkText("WRONG!", btn.GlobalPosition + Vector2.Up*50+ Vector2.Right * 450, Colors.Red);
		plonk.FloatUp();
		
		if (CurrentReward == POINT_REWARD)
		{
			CurrentReward = POINT_REWARD_OTHER;
			NowGuessing = Teams.Other(NowGuessing);
			GameTimer.Instance.SetTime(60);
			GameTimer.Instance.Start();
		}
		else
		{
			await GameTimeFlow.Stop(2000);
			await TryLoadNextQuestion();
		}
	}
}
