namespace TextAdventure;

public class Player : Character
{
	public Player(string name, int maxHealth, int minDmg, int maxDmg) : base(name, maxHealth, minDmg, maxDmg)
	{
	}

	public override void DisplayName() => Utils.ColorizeWrite(Name, ConsoleColor.Green);
}