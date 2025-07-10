using System.Threading.Tasks;
using Godot;

namespace SpireQuiz.Scripts.Sections;

public abstract partial class Section : Control
{
	[Export] protected RichTextLabel RulesLabel;
	[Export] protected RichTextLabel QuestionLabel;
	[Export] protected RichTextLabel NowGuessingLabel;
	[Export] protected TextureButton StartButton;
	public abstract SectionType SectionType { get; set; }
	protected abstract string Rules { get; }
	
	protected int QuestionIndex;
	protected TeamColor _nowGuessing;

	protected TeamColor NowGuessing
	{
		get => _nowGuessing;
		set {
			_nowGuessing = value;
			NowGuessingLabel.Text = $"Now Guessing: [color=#{Teams.ColorForTeam[value]}]{value}[/color]";
		}
	}


	public abstract Task Begin();

	public virtual async Task End()
	{
		Visible = false;
		Game.Instance.SectionPicker.Visible = true;
	}
	
	public async void ForceEnd()
	{
		await End();
	}
}