namespace TextAdventure;

public class Location
{
	public string Name { get; }
	private readonly string _desc;

	public Location(string name, string desc)
	{
		Name = name;
		_desc = desc;
	}

	public override string ToString()
	{
		return $"{Name} - {_desc}.";
	}
}