using System.ComponentModel.DataAnnotations.Schema;

namespace BookLibraryAPI.Models;

public class Author
{
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public int Id { get; set; }

	public string Name { get; set; } = string.Empty;
	public List<Book> Books { get; set; } = [];
}