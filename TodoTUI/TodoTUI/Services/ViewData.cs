using Spectre.Console;

namespace Todo.Services;

public class ViewData
{
	public readonly IList<TodoItem> TodoItems = GetTodoItems();

	private static List<TodoItem> GetTodoItems()
	{
		using var db = new TodoContext();
		var todos = db.Todos.OrderByDescending(t => t.Id).ToList();
		return todos;
	}

	public void DisplayTodoTable()
	{
		var table = new Table();
		table.AddColumn("Id");
		table.AddColumn("Text");
		table.AddColumn("Is Completed");
		table.AddColumn("DueDate");

		foreach (var item in TodoItems)
		{
			table.AddRow(
				item.Id.ToString(),
				item.Text ?? "",
				item.IsCompleted == false ? "No" : "Yes",
				item.DueDate.ToString() ?? ""
			);
		}

		AnsiConsole.Write(table);
	}
}