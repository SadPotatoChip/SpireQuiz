using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Godot;
using SpireKnight.Scripts.Audio;
using SpireQuiz.Scripts.Sections.MultiChoice;
using SpireQuiz.Scripts.TopBar;

namespace SpireQuiz.Scripts.Sections.Trivia;

public partial class SectionTrivia : Section
{
	public override SectionType SectionType { get; set; } = SectionType.Trivia;

	[Export] private SoundQueue CorrectSound;
	[Export] private SoundQueue IncorrectSound;
	[Export] private TextureButton StartButton;


	protected override string Rules =>
		"The team 4 minutes to answer as many questions as they can. The team members rotate after each question.\n" +
		$"A correct answer gives {POINT_REWARD} points, a wrong answer gives {POINT_LOSS}.\nYou may PASS.\n";

	private const int POINT_REWARD = 2;
	private const int POINT_LOSS = -1;

	private List<TriviaQuestion> AllQuestions;
	private TriviaQuestion CurrentQuestion;
	
	public override async Task Begin()
	{
		AllQuestions = TriviaQuestion.AllQuestions;
		Visible = true;
		RulesLabel.Text = Rules;
		StartButton.Visible = true;
	}

	public async void StartButtonPressed()
	{
		StartButton.Visible = false;
		QuestionIndex = -1;
		CurrentQuestion = null;
		NowGuessing = TeamColor.Red;
		GameTimer.Instance.SetTime(240);
		GameTimer.Instance.Start();
		await TryLoadNextQuestion();
	}

	public async void NextTeamButtonPressed()
	{
		NowGuessing = Teams.Other(NowGuessing);
		await TryLoadNextQuestion();
		GameTimer.Instance.SetTime(240);
		GameTimer.Instance.Start();
	}

	public async void WrongButtonPressed()
	{
		GameInformation.Instance.GiveScore(NowGuessing, POINT_LOSS);
		await TryLoadNextQuestion();
	}
	
	public async void CorrectButtonPressed()
	{
		GameInformation.Instance.GiveScore(NowGuessing, POINT_REWARD);
		CurrentQuestion.Answered = true;
		await TryLoadNextQuestion();
	}
	
	public async void PassButtonPressed()
	{
		await TryLoadNextQuestion();
	}

	private async Task TryLoadNextQuestion()
	{
		var unanswered = AllQuestions.Where(q => q.Answered == false);
		if (unanswered.Count() == 0)
		{
			await End();
			return;
		}
		var min = unanswered.Min(q => q.TimesAppeared);
		CurrentQuestion = unanswered.FirstOrDefault(q => q.TimesAppeared == min);
		CurrentQuestion.TimesAppeared++;
		
		QuestionLabel.Text = CurrentQuestion.Text;
	}
}