using System;
using Godot;
using SpireKnight.Scripts.Audio;

namespace SpireQuiz.Scripts.TopBar;

public partial class GameTimer : Control
{
	public static GameTimer Instance;
	
	[Export] private TextEdit TimerText;
	[Export] private SoundQueue TickSound;
	[Export] private SoundPool DoneSound;
	[Export] private SoundPool ForceStopSound;

	private int Time
	{
		get
		{
			var numbers = TimerText.Text.Split(":");
			if (numbers.Length == 1)
			{
				int.TryParse(numbers[0], out var s);
				return s;
			}
			else
			{
				int.TryParse(numbers[0], out var m);
				int.TryParse(numbers[1], out var s);
				return m * 60 + s;
			}
		}
	}

	private bool IsRunning;
	private double RunTime;

	public override void _Ready()
	{
		Instance = this;
	}

	public override void _Process(double delta)
	{
		if (IsRunning)
		{
			RunTime += delta;
			if (RunTime >= 0)
			{
				RunTime -= 1;
				TickDown();
				if (Time <= 0)
				{
					Finish();
				}
			}
		}
	}

	public void Start()
	{
		IsRunning = true;
		RunTime = 0;
	}

	public void Stop()
	{
		ForceStopSound.PlayRandomSound();
		Finish();
	}
	
	public void SetTime(int s)
	{
		s = Math.Max(s, 0);
		
		if (s < 60)
		{
			TimerText.Text = s.ToString();
		}
		else
		{
			TimerText.Text = (s/60) +":"+ (s%60<10?"0":"")+ (s%60);
		}
		
	}
	
	private void Finish()
	{
		IsRunning = false;
		RunTime = 0;
	}
	
	private void TickDown()
	{
		var t = Time;
		
		if (t == 1)
		{
			DoneSound.PlayRandomSound();
		}else if (t < 10)
		{
			TickSound.PlaySound();
		}
		
		SetTime(t-1);
		
	}
}