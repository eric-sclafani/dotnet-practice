namespace TextAdventure;

public class Location
{
	public string Name { get; }
	public readonly string Desc;
	private readonly Combat? _combat;

	public Location(string name, string desc, Combat? combat = null)
	{
		Name = name;
		Desc = desc;
		_combat = combat;
	}

	public override string ToString() => Name;

	public void BeginCombat()
	{
		_combat?.Begin();
	}
}

public class Locations
{
	public Location CurrentPlayerLocation { get; private set; }
	private readonly Player _player;
	private readonly IList<Location> _allLocations;

	public Locations(Player player)
	{
		_player = player;
		_allLocations = InitLocations();
		CurrentPlayerLocation = GetLocation("Home");
	}


	public void DisplayLocations()
	{
		foreach (var loc in _allLocations!.Where(loc => loc != CurrentPlayerLocation))
		{
			Console.WriteLine(loc);
		}
	}

	public Location? TryMovePlayerToLocation(string locationName)
	{
		if (IsValidLocation((locationName)))
		{
			var location = GetLocation(locationName);

			if (location != CurrentPlayerLocation)
			{
				CurrentPlayerLocation = location;
				return CurrentPlayerLocation;
			}
		}

		return null;
	}

	private Location GetLocation(string locationName)
	{
		return _allLocations.First(loc => loc.Name == locationName);
	}

	public bool IsValidLocation(string locationName)
	{
		return _allLocations.Any(loc => loc.Name == locationName);
	}

	private IList<Location> InitLocations()
	{
		IList<Location> locations = [];
		locations.Add(new Location("Home",
			"Your starting position. You are safe here, as safe as you can be, at least."));

		locations.Add(
			new Location(
				"Train City",
				"A city strangled by intertwining elevated rail lines in all directions.",
				new Combat(_player, new Enemy("Goblin", 10, 1, 5))
			)
		);

		locations.Add(
			new Location(
				"Green Meadows",
				"A vast sea of green studded with large pine trees and dense shrubbery.",
				new Combat(_player, new Enemy("Mutated Mantis", 6, 1, 12))
			)
		);

		locations.Add(
			new Location(
				"Samson's Beach",
				"A polluted mess hole of a beach, complete with greenish water.",
				new Combat(_player, new Enemy("Land Shark", 15, 5, 10))
			)
		);

		locations.Add(
			new Location(
				"Haven Mall",
				"A large abandoned mall with mountains of rotting furniture.",
				new Combat(_player, new Enemy("Duende", 5, 7, 15))
			)
		);
		return locations;
	}
}