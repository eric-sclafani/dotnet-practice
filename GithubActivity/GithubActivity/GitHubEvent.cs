using System.Text.Json.Serialization;

namespace GithubActivity;

public class GitHubEvent
{
	[JsonPropertyName("id")] public string? Id { get; set; }

	[JsonPropertyName("type")] public string? Type { get; set; }

	[JsonPropertyName("actor")] public Actor? Actor { get; set; }

	[JsonPropertyName("repo")] public Repo? Repo { get; set; }

	[JsonPropertyName("created_at")] public DateTime CreatedAt { get; set; }

	[JsonPropertyName("payload")]
	public object? Payload { get; set; } // You can create specific models for different event types
}

public class Actor
{
	[JsonPropertyName("id")] public int Id { get; set; }

	[JsonPropertyName("login")] public string? Login { get; set; }

	[JsonPropertyName("avatar_url")] public string? AvatarUrl { get; set; }
}

public class Repo
{
	[JsonPropertyName("id")] public int Id { get; set; }

	[JsonPropertyName("name")] public string? Name { get; set; }

	[JsonPropertyName("url")] public string? Url { get; set; }
}