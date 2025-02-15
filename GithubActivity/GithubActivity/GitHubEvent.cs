using System.Text.Json;
using System.Text.Json.Serialization;

namespace GithubActivity;

public class GitHubEvent
{
	[JsonPropertyName("id")] public string? Id { get; set; }

	[JsonPropertyName("type")] public string? Type { get; set; }

	[JsonPropertyName("repo")] public Repo? Repo { get; set; }

	[JsonPropertyName("created_at")] public DateTime CreatedAt { get; set; }

	[JsonPropertyName("payload")] public dynamic? Payload { get; set; }

	public override string ToString()
	{
		try
		{
			var payload = JsonSerializer.Deserialize<dynamic>(Payload);

			var eventString = Type switch
			{
				"PushEvent" => $"Pushed {payload.GetProperty("size")} commits to {Repo?.Name}",
				"IssuesEvent" => $"Opened a new issue in {Repo?.Name}",
				"CreateEvent" => $"Created a new branch in {Repo?.Name}",
				"PullRequestEvent" => $"Created a new pull request in {Repo?.Name}",
				"WatchEvent" => $"Starred {Repo?.Name}",
				_ => ""
			};
			return eventString;
		}
		catch
		{
			return "";
		}
	}
}

public class Repo
{
	[JsonPropertyName("id")] public int Id { get; set; }

	[JsonPropertyName("name")] public string? Name { get; set; }

	[JsonPropertyName("url")] public string? Url { get; set; }
}