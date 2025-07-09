using System;
using System.Collections.Generic;
using Godot;

namespace SpireKnight.Scripts.Audio;

[Tool]
public partial class SoundPool : Node
{
	private List<SoundQueue> _sounds = new ();
	private Random Rand = new Random();

	public override void _Ready()
	{
		foreach (var child in GetChildren())
		{
			if (child is SoundQueue soundQueue)
			{
				_sounds.Add(soundQueue);
			}

		}
	}

	public int PlayRandomSound()
	{
		int index = 0;
		if (_sounds.Count > 1)
		{
			index = Rand.Next(_sounds.Count);
		}
		var duration = _sounds[index].PlaySound();
		return duration;
	}
}