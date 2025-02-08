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
			var playerDmg = _player.DealDmg();
			_enemy.ReceiveDamage(playerDmg);

			_player.DisplayName();
			Console.Write($" deals {playerDmg} to ");
			_enemy.DisplayName();
			Console.Write("\n");

			Thread.Sleep(800);

			var enemyDmg = _enemy.DealDmg();
			_player.ReceiveDamage(enemyDmg);

			_enemy.DisplayName();
			Console.Write($" deals {enemyDmg} to ");
			_player.DisplayName();
			Console.Write("\n");
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