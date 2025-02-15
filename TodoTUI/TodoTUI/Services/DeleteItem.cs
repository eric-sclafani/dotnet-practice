using Spectre.Console;

namespace Todo.Services;

public static class DeleteItem
{
	public static async void Init(IList<TodoItem> todos)
	{
		var id = AnsiConsole.Prompt(new TextPrompt<int>("Type the ID to delete: "));
		var result = await DeleteTodoItem(id, todos);
		AnsiConsole.Markup(result == 1
			? "[green]Success! Item deleted[/].\n"
			: $"[red]record with id: {id} not found[/]");
	}

	private static async Task<int> DeleteTodoItem(int id, IList<TodoItem> todos)
	{
		await using var db = new TodoContext();
		if (todos.Count > 0)
		{
			try
			{
				var todoItem = todos.Where(t => t.Id == id)?.First();

				if (todoItem is null)
				{
					return 0;
				}

				db.Remove(todoItem);
				await db.SaveChangesAsync();
				return 1;
			}
			catch (Exception e)
			{
				return 0;
			}
		}


		return 0;
	}
}