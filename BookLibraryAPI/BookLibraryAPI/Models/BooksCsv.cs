namespace BookLibraryAPI.Models;

public class BooksCsv
{
	public string bookID { get; set; } = string.Empty;
	public string title { get; set; } = string.Empty;
	public string authors { get; set; } = string.Empty;
	public string average_rating { get; set; } = string.Empty;
	public string isbn { get; set; } = string.Empty;
	public string isbn13 { get; set; } = string.Empty;
	public string num_pages { get; set; } = string.Empty;
	public string ratings_count { get; set; } = string.Empty;
	public string text_reviews_count { get; set; } = string.Empty;
	public string publication_date { get; set; } = string.Empty;
	public string publisher { get; set; } = string.Empty;
}