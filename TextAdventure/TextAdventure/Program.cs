namespace TextAdventure;

internal class Program
{
	public static void Main()
	{
		var locations = new Locations();
		var player = new Player("Eric", 20, 1, 10);
		var goblin = new Enemy("Goblin", 10, 1, 5);

		var combat = new Combat(player, goblin);
		combat.Begin();

		// Intro(player.Name);
		//
		// var input = "";
		// do
		// {
		// 	Console.WriteLine("\n---Where would you like to go?---");
		// 	locations.DisplayLocations();
		// 	input = Utils.GetUserInput();
		//
		// 	if (!locations.IsValidLocation(input))
		// 	{
		// 		Console.WriteLine($"Location '{input}' not recognized.");
		// 	}
		// 	else
		// 	{
		// 		var moveMsg = $"You moved from {locations.CurrentPlayerLocation.Name} ";
		// 		var success = locations.TryMovePlayerToLocation(input);
		// 		if (success)
		// 		{
		// 			moveMsg += $"to {locations.CurrentPlayerLocation.Name}";
		// 			Console.WriteLine(moveMsg);
		// 		}
		// 		else
		// 		{
		// 			Console.WriteLine("You cannot move there since you already ARE there, silly!");
		// 		}
		// 	}
		// } while (input != "exit");
	}


	private static void Intro(string name)
	{
		var introText =
			$"Hello {name}, Welcome to your adventure! Here is a list of possible locations for you to move.";
		Console.WriteLine(introText);
	}
}