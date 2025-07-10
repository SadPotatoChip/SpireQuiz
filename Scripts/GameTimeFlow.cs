using System;
using System.Threading.Tasks;

namespace SpireQuiz.Scripts;

public static class GameTimeFlow
{
	public static double GameSpeed = 1f;
	private static Random Random = new Random();

	public static async Task StopFloat(float time)
	{
		await Stop((int)(time * 1000));
	}
	
	public static async Task Stop(int ms)
	{
		if (ms <= 0) return;
		
		await Task.Delay((int)(ms / GameSpeed));
	}

	public static async Task StopInSeconds(float s)
	{
		await Stop((int)(s * 1000));
	}
	
	public static async Task StopRandomized(int ms, int leeway = 50)
	{
		var duration = Math.Max(0,
			ms - leeway + Random.Next(leeway * 2));
		await Stop(duration);
	} 
}