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
			Status = "To Do",
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
			Utils.ErrorMsg($"Todo with id {id} not found");
		}
	}

	public async void Delete(int id)
	{
		var todo = _context.Todos.FirstOrDefault(t => t.Id == id);
		if (todo is not null)
		{
			_context.Remove(todo);
			await _context.SaveChangesAsync();
			Utils.SuccessMsg();
		}
		else
		{
			Utils.ErrorMsg($"Todo with id {id} not found");
		}
	}

	public async void MarkDone(int id)
	{
		var todo = _context.Todos.FirstOrDefault(t => t.Id == id);
		if (todo is not null)
		{
			todo.Status = "Done";
			await _context.SaveChangesAsync();
			Utils.SuccessMsg();
		}
		else
		{
			Utils.ErrorMsg($"Todo with id {id} not found");
		}
	}

	public async void MarkInProgress(int id)
	{
		var todo = _context.Todos.FirstOrDefault(t => t.Id == id);
		if (todo is not null)
		{
			todo.Status = "In Progress";
			await _context.SaveChangesAsync();
			Utils.SuccessMsg();
		}
		else
		{
			Utils.ErrorMsg($"Todo with id {id} not found");
		}
	}

	public void List(string? listOption = null)
	{
		var todos = listOption switch
		{
			"done" => _context.Todos.Where(t => t.Status == "Done").OrderByDescending(t => t.Id),
			"todo" => _context.Todos.Where(t => t.Status == "To Do").OrderByDescending(t => t.Id),
			"in-progress" => _context.Todos.Where(t => t.Status == "In Progress").OrderByDescending(t => t.Id),
			_ => _context.Todos.OrderByDescending(t => t.Id)
		};

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