using System.Threading.Tasks;
using Godot;

namespace SpireKnight.Scripts.Audio;

public partial class MusicController : Node
{
	public static MusicController Instance;

	[Export] private AudioStreamPlayer Player1;

	[Export] public AudioStream AmbientDay;
	[Export] public AudioStream AmbientNight;
	[Export] public AudioStream BossGreedus;
	[Export] public AudioStream BossPhysics;
	//[Export()] private AudioStream MenuMusic;

	private AudioStream QueuedSong;
	private bool IsFadingOut;
	private bool IsFadingIn;

	private float VOLUME;
	private const float VOLUME_FLOOR = -80f;
	private const float VOLUME_LERP = 30;

	public override void _Ready()
	{
		Instance = this;
		VOLUME = Player1.VolumeDb;
		Player1.VolumeDb = VOLUME_FLOOR;
	}

	public override void _Process(double delta)
	{
		
		if (IsFadingOut)
		{
			var lerp = (float)(delta * VOLUME_LERP);
			
			var vol = Player1.VolumeDb - lerp;
			if (vol > VOLUME_FLOOR)
			{
				Player1.VolumeDb -= lerp;
			}
			else
			{
				IsFadingOut = false;
				if (QueuedSong != null)
				{
					FadeIn();
				}
			}
		}

		if (IsFadingIn)
		{
			var lerp = (float)(delta * VOLUME_LERP);
			
			var vol = Player1.VolumeDb + lerp;
			if (vol < VOLUME)
			{
				Player1.VolumeDb += lerp;
			}
			else
			{
				IsFadingIn = false;
				Player1.VolumeDb = VOLUME;
			}
		}
	}

	public void Play(AudioStream song)
	{
		if (song == Player1.Stream)
		{
			return;
		}
		
		QueuedSong = song;
		if (Player1.IsPlaying())
		{
			FadeOutThenIn();
		}
		else
		{
			FadeIn();
			Player1.Play();
		}
		
	}

	public void Stop()
	{
		IsFadingIn = false;
		IsFadingOut = false;
		Player1.VolumeDb = VOLUME;
		Player1.Stop();
		Player1.Stream = null;
	}
	
	public void FadeOutThenIn()
	{
		IsFadingIn = false;
		IsFadingOut = true;
	}

	public void FadeIn()
	{
		Player1.VolumeDb = VOLUME_FLOOR;
		Player1.Stream = QueuedSong;
		Player1.Play();
		QueuedSong = null;
		IsFadingIn = true;
	}
	
}