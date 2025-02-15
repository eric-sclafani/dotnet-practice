using Spectre.Console;

namespace Todo.Services;

public static class UpdateItem
{
	public static void Init()
	{
		var id = AnsiConsole.Prompt(new TextPrompt<int>("Type the ID to delete: "));
		var (text, dueDate, isCompleted) = DisplayUpdateMenu();
		UpdateTodoItem(id, text, dueDate, isCompleted);
	}

	private static (string, DateTime?, bool) DisplayUpdateMenu()
	{
		var text = AnsiConsole.Prompt(new TextPrompt<string>("Updated todo text: "));
		var dateString = AnsiConsole.Prompt(
			new TextPrompt<string?>("Due date (mm-dd-yyyy): ")
				.Validate(ValidateDate)
				.AllowEmpty()
				.PromptStyle("green")
		);

		var isCompleted = AnsiConsole.Prompt(
			new TextPrompt<bool>("Completed?")
				.AddChoice(true)
				.AddChoice(false)
				.DefaultValue(true)
				.WithConverter(choice => choice ? "y" : "n"));

		DateTime? date = !string.IsNullOrEmpty(dateString) ? DateTime.Parse(dateString) : null;

		return (text, date, isCompleted);
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

	private static async void UpdateTodoItem(int id, string text, DateTime? dueDate, bool isCompleted)
	{
		await using var db = new TodoContext();

		try
		{
			var todo = db.Todos.First(t => t.Id == id);

			todo.Text = text;
			todo.IsCompleted = isCompleted;
			todo.DueDate = dueDate;

			await db.SaveChangesAsync();
		}
		catch (Exception e)
		{
			Console.WriteLine(e);
			throw;
		}
	}
}