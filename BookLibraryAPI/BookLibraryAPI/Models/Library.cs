namespace BookLibraryAPI.Models;

public class Library
{
	public IList<Book> Books { get; set; }
	public IList<Author> Authors { get; set; }
	public IList<BookLinkAuthor> BookLinkAuthor { get; set; }
}