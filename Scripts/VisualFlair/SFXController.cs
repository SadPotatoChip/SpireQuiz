using System.Collections.Generic;
using Godot;
using SpireKnight.Scripts.Helpers;

namespace SpireKnight.Scripts.VisualFlair;

public partial class SFXController : Node2D
{
	[Export(PropertyHint.Range, "0,20,0.5")]
	protected virtual double DestroyAfter { get; set; } = 10;

	[Export()]public bool IsOneShot { get; set; }
	private double TimeSinceCreated;
	
	protected List<GpuParticles2D> GpuParticles;
	
	public override void _Ready()
	{
		Initialize();
		
		if (IsOneShot)
		{
			foreach (var particle in GpuParticles)
			{
				particle.OneShot = true;
				particle.Emitting = true;
			}
		}
	}

	public sealed override void _Process(double delta)
	{
		if (DestroyAfter > 0)
		{
			TimeSinceCreated += delta;
			if (TimeSinceCreated >= DestroyAfter)
			{
				QueueFree();
			}
		}
	}

	public void SetEmitting(bool emitting)
	{
		foreach (GpuParticles2D particle in GpuParticles)
		{
			particle.Emitting = emitting;
		}
	}

	public void DestroyAfterDelay(double delay = 10)
	{
		DestroyAfter = delay;
	}
	
	private void Initialize()
	{

	}

	
}
