namespace TextAdventure;

public static class Utils
{
	public static string GetUserInput()
	{
		Console.Write(">>> ");
		var input = Console.ReadLine()?.Trim();
		return input ?? "";
	}

	public static void ColorizeWrite(string text, ConsoleColor color)
	{
		Console.ForegroundColor = color;
		Console.Write(text);
		Console.ResetColor();
	}
}