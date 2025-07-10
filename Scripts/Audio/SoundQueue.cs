using System;
using System.Collections.Generic;
using Godot;
using SpireKnight.Scripts.Helpers;

namespace SpireKnight.Scripts.Audio;

[Tool]
public partial class SoundQueue : Node
{
	private int _next = 0;
	private List<AudioStreamPlayer> _audioStreamPlayers = new List<AudioStreamPlayer>();
	[Export] public int Count { get; set; } = 4;
	[Export(PropertyHint.Range, "0,1,0.01")] public float PitchShift { get; set; }
	private float InitialPitch;

	public override void _Ready()
	{
		if (GetChildCount() == 0)
		{
			GD.Print("No AudioStreamPlayer child found.");
			return;
		}

		var child = GetChild(0);
		if (child is AudioStreamPlayer audioStreamPlayer)
		{
			_audioStreamPlayers.Add(audioStreamPlayer);
			InitialPitch = audioStreamPlayer.PitchScale;
			for (int i = 0; i < Count; i++)
			{
				AudioStreamPlayer duplicate = audioStreamPlayer.Duplicate() as AudioStreamPlayer;
				AddChild(duplicate);
				_audioStreamPlayers.Add(duplicate);
			}
		}
	}

	public override string[] _GetConfigurationWarnings()
	{
		if (GetChildCount() == 0)
		{
			return new string[] { "No children found. Expected one AudioStreamPlayer child." };
		}

		if (GetChild(0) is not AudioStreamPlayer)
		{
			return new string[] { "Expected first child to be an AudioStreamPlayer." };

		}

		return base._GetConfigurationWarnings();
	}

	public void SetSound(AudioStream stream)
	{
		foreach (var player in _audioStreamPlayers)
		{
			player.Stream = stream;
		}
	}

	public void Play()
	{
		PlaySound();
	}
	
	public int PlaySound(AudioStream sound = null, float? overridePitchShift = null)
	{
		if (!_audioStreamPlayers[_next].Playing)
		{
			var player = _audioStreamPlayers[_next++];
			if (sound != null)
			{
				player.SetStream(sound);
			}

			if (player.Stream != null)
			{
				player.Play();
			}
			_next %= _audioStreamPlayers.Count;
		}

		var stream = _audioStreamPlayers[0]?.Stream;
		return stream == null? 0 : (int)(stream.GetLength() * 1000);
	}

	public void Stop()
	{
		foreach (var player in _audioStreamPlayers)
		{
			player.Stop();
		}
	}
}