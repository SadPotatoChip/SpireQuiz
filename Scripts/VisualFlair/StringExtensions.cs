using Godot;

namespace SpireKnight.Scripts.VisualFlair;

public static class StringExtensions
{
	public static string RichWrapColor(this string s, Color color)
	{
		return s.RichWrap("color=#" + color.ToHtml(false), "color");
	}
	
	public static string RichWrapColor(this string s, string color)
	{
		return s.RichWrap("color=#" + color, "color");
	}
	
	public static string RichWrap(this string s, string word)
	{
		return "[" + word + "]" + s +"[/" + word + "]";
	}
	
	public static string RichWrap(this string s, string open, string close)
	{
		return "[" + open + "]" + s +"[/" + close + "]";
	}
}