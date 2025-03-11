using System.ComponentModel.DataAnnotations.Schema;

namespace BookLibraryAPI.Models;

public class Book
{
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public int Id { get; set; }

	public string Title { get; set; } = string.Empty;
	public double AverageRating { get; set; }
	public int Isbn { get; set; }
	public int Isbn13 { get; set; }
	public int NumPages { get; set; }
	public int RatingsCount { get; set; }
	public int TextReviewsCount { get; set; }
	
	public List<Author> Authors { get; set; } = [];
}