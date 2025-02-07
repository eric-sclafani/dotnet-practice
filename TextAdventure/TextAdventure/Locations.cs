namespace TextAdventure;

public class Locations
{
	public Location CurrentPlayerLocation { get; private set; }
	private readonly IList<Location> _allLocations;

	public Locations()
	{
		_allLocations = InitLocations();
		CurrentPlayerLocation = GetLocation("Home");
	}

	private static IList<Location> InitLocations()
	{
		IList<Location> locations = [];
		locations.Add(
			new Location("Home", "Your starting position. You are safe here, as safe as you can be, at least."));
		locations.Add(new Location("Green Meadows", "Lush sea of green for all the eyes can see."));
		locations.Add(new Location("Train City",
			"A large metropolis strangled by elevated rail lines twisting and turning"));
		return locations;
	}

	public void DisplayLocations()
	{
		foreach (var loc in _allLocations!.Where(loc => loc != CurrentPlayerLocation))
		{
			Console.WriteLine(loc);
		}
	}

	public bool TryMovePlayerToLocation(Player player, string locationName)
	{
		if (IsValidLocation((locationName)))
		{
			var location = GetLocation(locationName);

			if (location != CurrentPlayerLocation)
			{
				CurrentPlayerLocation = location;
				return true;
			}
		}

		return false;
	}

	private Location GetLocation(string locationName)
	{
		return _allLocations.First(loc => loc.Name == locationName);
	}

	public bool IsValidLocation(string locationName)
	{
		return _allLocations.Any(loc => loc.Name == locationName);
	}
}

public class Location
{
	public string Name { get; }
	public readonly string Desc;

	public Location(string name, string desc)
	{
		Name = name;
		Desc = desc;
	}

	public override string ToString() => Name;
}