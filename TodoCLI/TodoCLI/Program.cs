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
					MarkInProgress(args, todoService);
					break;
				case "mark-done":
					MarkDone(args, todoService);
					break;
				case "list":
					List(args, todoService);
					break;
				case "help":
					break;
				default:
					Utils.ErrorMsg(
						$"Invalid command '{action}'. Type 'help' to see a list of valid commands");
					break;
			}
		}
		else
		{
			Utils.ErrorMsg("no arguments provided. Type 'help' to see a list of valid commands");
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

	private static void MarkDone(string[] args, TodoService service)
	{
		var id = Utils.ValidateId(args);
		service.MarkDone(id);
	}

	private static void MarkInProgress(string[] args, TodoService service)
	{
		var id = Utils.ValidateId(args);
		service.MarkInProgress(id);
	}

	private static void List(string[] args, TodoService service)
	{
		var listOption = Utils.ValidateListOption(args);
		if (listOption is not null)
		{
			service.List(listOption);
		}
		else if (args.Length == 1)
		{
			service.List();
		}
	}

	private static void Delete(string[] args, TodoService service)
	{
		var id = Utils.ValidateId(args);
		service.Delete(id);
	}
}