namespace TextAdventure;

public class Player
{
	public string Name { get; }
	public Location? CurrentLocation { get; set; }
	public Location? PreviousLocation { get; set; }

	public Player(string name)
	{
		Name = name;
	}
}