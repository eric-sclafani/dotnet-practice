using TodoCLI.Models;
using TodoCLI.Services;

namespace TodoCLI;

public class Program
{
	public static void Main(string[] args)
	{
		if (args.Length > 0)
		{
			var action = args[0];

			using var context = new TodoContext();
			var todoService = new TodoService(context);

			switch (action)
			{
				case "add":
					Add(args, todoService);
					break;
				case "update":
					Update(args, todoService);
					break;
				case "delete":
					Delete(args, todoService);
					break;
				case "mark-in-progress":
					break;
				case "mark-done":
					break;
				case "list":
					List(args, todoService);
					break;
				case "help":
					break;
				default:
					Console.WriteLine(
						$"Invalid command: '{action}'. Type 'help' to see a list of valid commands");
					break;
			}
		}
		else
		{
			Console.WriteLine("Error: no arguments provided. Type 'help' to see a list of valid commands");
		}
	}

	private static void Add(string[] args, TodoService service)
	{
		var descr = Utils.ValidateDescription(args, 1);
		if (descr is not null)
		{
			service.Add(descr);
		}
	}

	private static void Update(string[] args, TodoService service)
	{
		var id = Utils.ValidateId(args);
		var newDescr = Utils.ValidateDescription(args, 2);
		if (newDescr is not null)
		{
			service.Update(id, newDescr);
		}
	}

	private static void List(string[] args, TodoService service)
	{
		service.List();
	}

	private static void Delete(string[] args, TodoService service)
	{
		var id = Utils.ValidateId(args);
	}
}