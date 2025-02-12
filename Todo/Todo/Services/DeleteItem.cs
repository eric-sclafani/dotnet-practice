using Spectre.Console;

namespace Todo.Services;

public static class DeleteItem
{
	public static async void Init(IList<TodoItem> todos)
	{
		var id = AnsiConsole.Prompt(new TextPrompt<int>("Type the ID to delete: "));
		var result = await DeleteTodoItem(id, todos);
		if (result == 1)
		{
			AnsiConsole.Markup("[green]Success! Item deleted[/].\n");
		}
	}

	private static async Task<int> DeleteTodoItem(int id, IList<TodoItem> todos)
	{
		await using var db = new TodoContext();
		var todoItem = todos.Where(t => t.Id == id)?.First();
		try
		{
			if (todoItem is not null)
			{
				db.Remove(todoItem);
				await db.SaveChangesAsync();
				return 1;
			}
		}
		catch (Exception e)
		{
			return 0;
		}

		return 0;
	}
}