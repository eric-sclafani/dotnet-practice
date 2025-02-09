namespace TextAdventure;

public class Combat
{
	private readonly Player _player;
	private readonly Enemy _enemy;

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
		Console.WriteLine(". Prepare for battle!\n");

		while (_player.IsAlive() && _enemy.IsAlive())
		{
			_player.DealDmgTo(_enemy);

			if (_enemy.IsAlive())
			{
				_enemy.DealDmgTo(_player);
				Console.WriteLine();
				Thread.Sleep(2000);
			}
		}

		if (_player.IsAlive())
		{
			_player.DisplayName();
			Console.Write(" has defeated ");
			_enemy.DisplayName();
			Console.Write(".");
			Thread.Sleep(1000);
		}
		else
		{
			Console.WriteLine("You have lost this battle.");
		}
	}
}