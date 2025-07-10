using System.Threading.Tasks;
using Godot;

namespace SpireQuiz.Scripts.Sections;

public abstract partial class Section : Control
{
	[Export] protected RichTextLabel RulesLabel;
	public abstract SectionType SectionType { get; set; }
	protected abstract string Rules { get; }

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