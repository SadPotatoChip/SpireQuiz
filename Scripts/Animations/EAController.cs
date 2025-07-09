using System.Collections.Generic;
using System.Linq;
using Godot;
using SpireKnight.Scripts.Audio;

namespace SpireKnight.Scripts.Helpers.Animations;

public partial class EAController : Node2D
{
	public static EAController Instance;

	#region Audio

	[Export(PropertyHint.NodeType)] public SoundPool ImpactSoundPool { get; set; }
	[Export(PropertyHint.NodeType)] public SoundPool WooshSoundPool { get; set; }

	#endregion
	
	private readonly Dictionary<Node, ExclusiveAnimation> RunningAnimations = new ();
	private readonly Queue<ExclusiveAnimation> EnqueuedAnimations = new();
	private readonly List<Node> FinishedAnimationsThisFrame = new();

	public bool SnapAllAnimations;
	
	public override void _Ready()
	{
		Instance = this;
	}

	public override void _Process(double delta)
	{
		StartEnqueuedAnimations();

		if (false == RunningAnimations.Any())
		{
			return;
		}

		foreach (var (node, anim) in RunningAnimations)
		{
			if (false == IsInstanceValid(anim.Target))
			{
				RunningAnimations.Remove(node);
				continue;
			}

			if (anim.IsStopped 
			    || anim.TotalDuration <= delta
			    || anim.TotalDuration <= anim.Time)
			{
				FinishedAnimationsThisFrame.Add(node);
			}
			else
			{
				anim.Process(delta);
			}
		}

		foreach (var node in FinishedAnimationsThisFrame)
		{
			FinishAnimatingNode(node);
		}
		FinishedAnimationsThisFrame.Clear();
	}

	public void ExclusiveAnimate(ExclusiveAnimation anim)
	{
		if (SnapAllAnimations)
		{
			anim.Start();
			anim.Process(anim.TotalDuration);
		}
		else
		{
			EnqueuedAnimations.Enqueue(anim);
		}
	}

	public void ForceStopAnimatingNode(Node node)
	{
		if (RunningAnimations.TryGetValue(node, out var animation))
		{
			animation.Stop();
			animation.Finish();
			RunningAnimations.Remove(node);
		}
	}
	
	#region Helpers

	private void FinishAnimatingNode(Node node)
	{
		RunningAnimations[node].Finish();
		RunningAnimations.Remove(node);
	}

	private void StartEnqueuedAnimations()
	{
		var n = EnqueuedAnimations.Count;
		for (int i = 0; i < n; i++)
		{
			var anim = EnqueuedAnimations.Dequeue();
			TryStartAnimation(anim);
		}
	}
	
	private void TryStartAnimation(ExclusiveAnimation newAnim)
	{
		if (false == IsInstanceValid(newAnim.Target))
		{
			return;
		}
		
		RunningAnimations.TryGetValue(newAnim.Target, out var oldAnim);
		
		if (oldAnim != null && newAnim.Priority < RunningAnimations[newAnim.Target].Priority)
		{
			return;
		}

		RunningAnimations[newAnim.Target] = newAnim;
		oldAnim?.Stop();
		newAnim.Start();
	}

	#endregion
	
	public void Clear()
	{
		foreach (var pair in RunningAnimations)
		{
			pair.Value.Stop();
		}

		RunningAnimations.Clear();
	}

}