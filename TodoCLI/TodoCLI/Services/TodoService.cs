using TodoCLI.Models;
using Spectre.Console;

namespace TodoCLI.Services;

public class TodoService
{
	private readonly TodoContext _context;

	public TodoService(TodoContext context)
	{
		_context = context;
	}

	public async void Add(string text)
	{
		var todo = new Todo()
		{
			Description = text,
			CreatedAt = DateTime.Today,
			UpdatedAt = DateTime.Today
		};

		_context.Add(todo);
		await _context.SaveChangesAsync();
		Utils.SuccessMsg();
	}

	public async void Update(int id, string newDescr)
	{
		var todo = _context.Todos.FirstOrDefault(t => t.Id == id);
		if (todo is not null)
		{
			todo.Description = newDescr;
			await _context.SaveChangesAsync();
			Utils.SuccessMsg();
		}
		else
		{
			Utils.ErrorMsg($"id {id} not found");
		}
	}

	// public async void Delete(int id)
	// {
	// }

	// public async void MarkDone()
	// {
	// }
	//
	// public async void MarkInProgress()
	// {
	// }

	public void List(string? sortBy = null)
	{
		var todos = _context.Todos.OrderByDescending(t => t.Id);


		var table = new Table();
		table.AddColumns(
			["Id", "Description", "Status", "CreatedAt", "UpdatedAt"]
		);
		foreach (var todo in todos)
		{
			table.AddRow(
				todo.Id.ToString(),
				todo.Description ?? "",
				todo.Status ?? "",
				todo.CreatedAt.ToShortDateString(),
				todo.UpdatedAt.ToShortDateString()
			);
		}

		AnsiConsole.Write(table);
	}
}