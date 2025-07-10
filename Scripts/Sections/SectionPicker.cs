using System.Linq;
using System.Threading.Tasks;
using Godot;

namespace SpireQuiz.Scripts.Sections;

public partial class SectionPicker : Control
{
	[Export] private Section[] Sections;

	public override void _Ready()
	{
		foreach (var s in Sections)
		{
			s.Visible = false;
		}
	}

	public async void MultiChoiceClicked()
	{
		await StartSection(SectionType.MultiChoiceQuestions);
	}
	
	public async void CategoryClicked()
	{
		await StartSection(SectionType.FillTheCategory);
	}
	
	public async void GuessNumberClicked()
	{
		await StartSection(SectionType.GuessTheNumber);
	}
	
	public async void TriviaClicked()
	{
		await StartSection(SectionType.Trivia);
	}

	private async Task StartSection(SectionType type)
	{
		Visible = false;
		await Sections.First(s => s.SectionType == type).Begin();
	}
}

public enum SectionType
{
	MultiChoiceQuestions = 0,
	FillTheCategory = 1,
	GuessTheNumber = 2,
	Trivia = 3
}