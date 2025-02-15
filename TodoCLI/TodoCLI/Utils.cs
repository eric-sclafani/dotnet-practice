using Spectre.Console;

namespace TodoCLI;

public static class Utils
{
	public static void SuccessMsg(string msg = "Success")
	{
		Console.ForegroundColor = ConsoleColor.Green;
		Console.WriteLine(msg);
		Console.ResetColor();
	}

	public static void ErrorMsg(string msg)
	{
		Console.ForegroundColor = ConsoleColor.Red;
		Console.WriteLine($"Error: {msg}");
		Console.ResetColor();
	}

	public static int ValidateId(string[] args)
	{
		var id = 0;
		try
		{
			var success = int.TryParse(args[1], out id);
			if (!success)
			{
				ErrorMsg("'id' is not a valid integer.");
			}
		}
		catch
		{
			ErrorMsg("field 'id' not found.");
		}

		return id;
	}

	public static string? ValidateDescription(string[] args, int index)
	{
		try
		{
			var newDescr = args[index];
			if (!string.IsNullOrWhiteSpace(newDescr))
			{
				return newDescr;
			}

			ErrorMsg("field 'description' must contain text.");
		}
		catch
		{
			ErrorMsg("field 'description' not found.");
		}

		return null;
	}
}