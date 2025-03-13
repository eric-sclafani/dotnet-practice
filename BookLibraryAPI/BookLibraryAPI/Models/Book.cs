using System.ComponentModel.DataAnnotations.Schema;

namespace BookLibraryAPI.Models;

public class Book
{
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public int Id { get; set; }

	public string Title { get; set; } = string.Empty;
	public double AverageRating { get; set; }
	public string Isbn { get; set; } = string.Empty;
	public string Isbn13 { get; set; } = string.Empty;
	public int NumPages { get; set; }
	public int RatingsCount { get; set; }
	public int TextReviewsCount { get; set; }
	public DateOnly PublicationDate { get; set; }
	public string Publisher { get; set; } = string.Empty;

	public IList<Author> Authors { get; set; } = [];
}