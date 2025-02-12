using Spectre.Console;

namespace Todo.Services;

public static class AddItem
{
	public static async void Init()
	{
		var (text, dueDate) = DisplayAddTodoMenu();
		var result = await AddTodoItem(text, dueDate);
		if (result == 1)
		{
			AnsiConsole.Markup("[green]Success! Data successfuly saved[/]\n");
		}
	}

	private static (string, DateTime?) DisplayAddTodoMenu()
	{
		var text = AnsiConsole.Prompt(new TextPrompt<string>("Todo text: "));
		var dateString = AnsiConsole.Prompt(
			new TextPrompt<string?>("Due date (mm-dd-yyyy): ")
				.Validate(ValidateDate)
				.AllowEmpty()
				.PromptStyle("green")
		);

		DateTime? date = !string.IsNullOrEmpty(dateString) ? DateTime.Parse(dateString) : null;

		return (text, date);
	}

	private static ValidationResult ValidateDate(string? dateString)
	{
		var success = ValidationResult.Success();

		if (string.IsNullOrWhiteSpace(dateString))
		{
			return success;
		}

		if (DateTime.TryParse(dateString, out var parsedDate))
		{
			return parsedDate < DateTime.Today
				? ValidationResult.Error("[red]Date must be today or in the future[/]")
				: success;
		}

		return ValidationResult.Error("[red]Invalid date format[/]");
	}


	private static async Task<int> AddTodoItem(string text, DateTime? dueDate)
	{
		try
		{
			await using var db = new TodoContext();
			var todo = new TodoItem()
			{
				Text = text,
				DueDate = dueDate,
				IsCompleted = false
			};
			await db.AddAsync(todo);
			await db.SaveChangesAsync();
			return 1;
		}
		catch (Exception e)
		{
			AnsiConsole.Markup($"[red]Something went wrong when adding todo item:[/]\n{e}");
			return 0;
		}
	}
}