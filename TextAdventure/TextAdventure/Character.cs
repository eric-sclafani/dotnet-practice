namespace TextAdventure;

public abstract class Character
{
	public readonly string Name;
	private int _maxHealth;
	private int _currentHealth;
	private int _minDmg;
	private int _maxDmg;


	protected Character(string name, int maxHealth, int minDmg, int maxDmg)
	{
		Name = name;
		_maxHealth = maxHealth;
		_currentHealth = maxHealth;
		_minDmg = minDmg;
		_maxDmg = maxDmg;
	}

	public bool IsAlive() => _currentHealth > 0;

	public void ReceiveDamage(int dmg)
	{
		var amount = _currentHealth - dmg;
		_currentHealth = amount > 0 ? amount : 0;
	}

	public void RecoverHealth(int health)
	{
		// TODO: improve the health calculation and console writing
		var oldHealth = _currentHealth;
		var amount = _currentHealth + health;
		_currentHealth = amount > _maxHealth ? _maxHealth : amount;

		Console.Write($"{Name} recovered ");
		Utils.ColorizeWrite(health.ToString(), ConsoleColor.Green);
		Console.Write($" health ({oldHealth}) => ({_currentHealth})");
	}

	public int DealDmg()
	{
		var rand = new Random();
		return rand.Next(_minDmg, _maxDmg + 1);
	}

	public void DisplayHealth()
	{
		Console.WriteLine($"{Name}: {_currentHealth}/{_maxHealth}");
	}

	public abstract void DisplayName();
}