using System.Text.Json;

namespace GithubActivity;

using System.Net.Http.Headers;

internal class Program
{
	public static async Task Main(string[] args)
	{
		using var client = InitHttpClient();
		var username = GetUsername(args);

		if (username is not null)
		{
			var userEvents = await ProcessUserEvents(client, username);

			if (userEvents is not null)
			{
				foreach (var e in userEvents)
				{
					Console.WriteLine(e);
				}
			}
		}
	}

	private static HttpClient InitHttpClient()
	{
		HttpClient client = new();

		// removes any existing Accept headers
		client.DefaultRequestHeaders.Accept.Clear();

		// adds an Accept header to specify that the client expects a response in GitHub's v3 API JSON format.
		client.DefaultRequestHeaders.Accept.Add(
			new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));

		client.DefaultRequestHeaders.Add("User-Agent", "Github User Activity App");
		return client;
	}

	private static async Task<IList<GitHubEvent>?> ProcessUserEvents(HttpClient client, string username)
	{
		try
		{
			var response = await client.GetStreamAsync($"https://api.github.com/users/{username}/events");
			var userEvents = await JsonSerializer.DeserializeAsync<IList<GitHubEvent>>(response);

			if (userEvents != null)
			{
				return userEvents;
			}
		}
		catch
		{
			ErrorMsg($"Username {username} not found on Github.");
		}

		return null;
	}

	private static string? GetUsername(string[] args)
	{
		string? username = null;
		try
		{
			username = args[0];
		}
		catch
		{
			ErrorMsg("Field 'username' not provided.");
		}

		return username;
	}

	private static void ErrorMsg(string text)
	{
		Console.ForegroundColor = ConsoleColor.Red;
		Console.WriteLine($"Error: {text}");
		Console.ResetColor();
	}
}