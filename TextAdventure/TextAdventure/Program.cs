namespace TextAdventure;

internal class Program
{
	public static void Main()
	{
		var locations = new Locations();
		var player = new Player("Eric");

		Intro(player.Name);

		var userInput = "";
		do
		{
			Console.WriteLine("\n---Where would you like to go?---");
			locations.DisplayLocations();
			userInput = GetUserInput();

			if (!locations.IsValidLocation(userInput))
			{
				Console.WriteLine($"Location '{userInput}' not recognized.");
			}
			else
			{
				var moveMsg = $"You moved from {locations.CurrentPlayerLocation.Name} ";
				var success = locations.TryMovePlayerToLocation(player, userInput);
				if (success)
				{
					moveMsg += $"to {locations.CurrentPlayerLocation.Name}";
					Console.WriteLine(moveMsg);
				}
				else
				{
					Console.WriteLine("You cannot move there since you already ARE there, silly!");
				}
			}
		} while (userInput != "exit");
	}


	private static void Intro(string name)
	{
		var introText =
			$"Hello {name}, Welcome to your adventure! Here is a list of possible locations for you to move.";
		Console.WriteLine(introText);
	}

	private static string GetUserInput()
	{
		Console.Write(">>> ");
		var input = Console.ReadLine()?.Trim();
		return input ?? "";
	}
}