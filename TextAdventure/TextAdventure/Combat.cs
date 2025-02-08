namespace TextAdventure;

public class Combat
{
	private Player _player;
	private Enemy _enemy;

	public Combat(Player player, Enemy enemy)
	{
		_player = player;
		_enemy = enemy;
	}

	public void Begin()
	{
		_player.DisplayName();
		Console.Write(" enters combat with a ");
		_enemy.DisplayName();
		Console.WriteLine(". Prepare for battle!");

		while (_player.IsAlive() && _enemy.IsAlive())
		{
			_player.DealDmgTo(_enemy);
			_enemy.DealDmgTo(_player);
			Thread.Sleep(1000);
		}

		if (_player.IsAlive())
		{
			_player.DisplayName();
			Console.Write(" has defeated ");
			_enemy.DisplayName();
			Console.Write(".");
		}
		else
		{
			Console.WriteLine("You have lost this battle.");
		}
	}
}