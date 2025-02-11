namespace TextAdventure;

internal class Program
{
	public static void Main()
	{
		var player = new Player("Eric", 30, 1, 10);
		var locations = new Locations(player);

		Intro(player.Name);

		string input;
		do
		{
			Console.WriteLine("---Where would you like to go?---");
			locations.DisplayLocations();
			input = Utils.GetUserInput();

			if (!locations.IsValidLocation(input))
			{
				Console.WriteLine($"Location '{input}' not recognized.");
			}
			else
			{
				var moveMsg = $"You moved from {locations.CurrentPlayerLocation.Name} ";
				var location = locations.TryMovePlayerToLocation(input);
				if (location is not null)
				{
					moveMsg += $"to {locations.CurrentPlayerLocation.Name}\n";
					Console.WriteLine(moveMsg);

					location.BeginCombat();
				}
				else
				{
					Console.WriteLine("You cannot move there since you already ARE there, silly!");
				}
			}
		} while (input != "exit");
	}


	private static void Intro(string name)
	{
		var introText =
			$"Hello {name}, Welcome to your adventure! Here is a list of possible locations for you to move.";
		Console.WriteLine(introText);
	}
}