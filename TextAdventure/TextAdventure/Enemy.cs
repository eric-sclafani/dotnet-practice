namespace TextAdventure;

public class Enemy : Character
{
	public Enemy(string name, int maxHealth, int minDmg, int maxDmg) : base(name, maxHealth, minDmg, maxDmg)
	{
		
	}
	
	public override void DisplayName() => Utils.ColorizeWrite(Name, ConsoleColor.Red);
}