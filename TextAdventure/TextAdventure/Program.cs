namespace TextAdventure;

internal class Program
{
	public static void Main()
	{
		var locations = GetLocations();
		var player = new Player("Eric")
		{
			CurrentLocation = locations["Home"],
			PreviousLocation = locations["Home"]
		};

		Intro(player.Name);

		var userInput = "";
		do
		{
			DisplayLocations(locations, player.CurrentLocation);
			userInput = GetUserInput();
			if (!IsValidLocation(userInput, locations))
			{
				Console.WriteLine($"Location '{userInput}' not recognized.");
			}
			else
			{
				player.PreviousLocation = player.CurrentLocation;
				var success = TryMovePlayerToLocation(player, locations[userInput]);
				if (success)
				{
					Console.Write($"You moved from {player.PreviousLocation.Name} to {player.CurrentLocation.Name}\n");
				}
				else
				{
					Console.WriteLine("You cannot move there since you already ARE there, silly!");
				}
			}
		} while (userInput != "exit");
	}

	private static bool IsValidLocation(string loc, Dictionary<string, Location> locations) =>
		locations.ContainsKey(loc);

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

	private static void DisplayLocations(Dictionary<string, Location> locations, Location? currentLocation)
	{
		Console.WriteLine("\n---Where would you like to go?---");
		foreach (var loc in locations.Where(loc => loc.Value != currentLocation))
		{
			Console.WriteLine(loc.Value);
		}

		Console.WriteLine();
	}

	private static Dictionary<string, Location> GetLocations()
	{
		Dictionary<string, Location> locations = new()
		{
			["Home"] = new Location(
				"Home",
				"Home base, where you originated from"
			),
			["Green Meadows"] = new Location(
				"Green Meadows",
				"A lush sea of green for all the eyes can see, with strange looking plant life and trees."
			),

			["Train City"] = new Location(
				"Train City",
				"A city entangled by long elevated rail lines that wrap around everything."
			),
		};

		return locations;
	}

	private static bool TryMovePlayerToLocation(Player player, Location location)
	{
		if (player.CurrentLocation != location)
		{
			player.CurrentLocation = location;
			return true;
		}

		return false;
	}
}