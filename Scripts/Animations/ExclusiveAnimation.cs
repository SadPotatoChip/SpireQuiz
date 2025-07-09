using System;
using System.Collections.Generic;
using System.Linq;
using Godot;
using SpireKnight.Scripts.Helpers.Animations.Mods;

namespace SpireKnight.Scripts.Helpers.Animations;

public abstract class ExclusiveAnimation
{
	public Node Target;

	public bool IsFinished { get; private set; }
	public bool IsStopped { get; protected set; }
	protected double Duration { get; set; }
	public double Time { get; protected set; }
	public double InitialDelay { get; protected set; }

	public double TotalDuration => Duration + InitialDelay;

	public ExclusiveAnimationPriority Priority = ExclusiveAnimationPriority.Normal;

	public List<ExclusiveAnimationMod> Mods = new ();
	
	[Signal]
	public delegate void ExclusiveAnimationEventHandler();
	public event ExclusiveAnimationEventHandler OnFinished;

	public abstract void Start();
	
	public virtual void Stop()
	{
		IsStopped = true;
	}

	public void Finish()
	{
		IsFinished = true;
		AfterFinished();
		
		OnFinished?.Invoke();
	}

	protected abstract void AfterFinished();

	public abstract void Process(double delta);

	protected Vector2 Lerp(Vector2 start, Vector2 end, double percent)
	{
		return (start + ((float)percent)*(end - start));
	}
	
	protected double Lerp(double start, double end, double percent)
	{
		return (start + percent*(end - start));
	}
	
	protected float Lerp(float start, float end, float percent)
	{
		return (start + percent*(end - start));
	}
	
	protected Color Lerp(Color start, Color end, float percent)
	{
		return new Color(Lerp(start.R, end.R, percent),
			Lerp(start.G, end.G, percent),
			Lerp(start.B, end.B, percent),
			Lerp(start.A, end.A, percent));
	}

	#region Easing Functions

	public static double easeOutQuart(double x) {
		return (1 - Math.Pow(1 - x, 4));
	}
	
	public static double easeInSine(double x) {
		return 1 - Math.Cos((x * Math.PI) / 2);
	}
	
	public static double linear(double x) {
		return x;
	}
	
	public static double easeOutCubic(double x) {
		return 1 - Math.Pow(1 - x, 3);
	}
	
	public static double easeInCubic(double x ) {
		return x * x * x;
	}

	public static double easeOutElastic(double x ){
		const double c4 = (2 * Math.PI) / 3;

		return x == 0 ? 0 : (x == 1 ? 1 : Math.Pow(2, -10 * x) * Math.Sin((x * 10 - 0.75) * c4) + 1);
	}
	
	public static double easeInOutElastic(double x ){
		const double c5 = (2 * Math.PI) / 4.5;

		return x == 0
			? 0
			: x == 1
				? 1
				: x < 0.5
					? -(Math.Pow(2, 20 * x - 10) * Math.Sin((20 * x - 11.125) * c5)) / 2
					: (Math.Pow(2, -20 * x + 10) * Math.Sin((20 * x - 11.125) * c5)) / 2 + 1;
	}


	#endregion
	
}