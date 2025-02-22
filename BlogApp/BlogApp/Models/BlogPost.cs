using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BlogApp.Models;

public class BlogPost
{
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public int Id { get; init; }

	[Required(ErrorMessage = "Content is required")]
	[StringLength(500)]
	public string? Content { get; set; }

	[Required(ErrorMessage = "Title is required")]
	[StringLength(50)]
	public string? Title { get; set; }

	public bool IsDraft { get; set; }
	public DateTime CreatedAt { get; init; } = DateTime.Now;
	public DateTime LastUpdatedAt { get; set; } = DateTime.Now;

	public string ShortenedContent()
	{
		const int threshold = 50;
		var shortened = Content;

		if (Content?.Length > threshold)
		{
			shortened = $"{Content[..50]}...";
		}

		return shortened ?? "";
	}
}