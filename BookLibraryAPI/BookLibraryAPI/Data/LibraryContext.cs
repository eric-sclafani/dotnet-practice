using BookLibraryAPI.Models;
using BookLibraryAPI.Utils;
using Microsoft.EntityFrameworkCore;

namespace BookLibraryAPI.Data;

public class LibraryContext : DbContext
{
	public DbSet<Book> Books { get; set; }
	public DbSet<Author> Authors { get; set; }

	public LibraryContext(DbContextOptions<LibraryContext> options) : base(options)
	{
	}

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		optionsBuilder.UseSqlite("Data Source=./Data/library.db");
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		EstablishRelationships(modelBuilder);
		//SeedData(modelBuilder);
	}

	private static void EstablishRelationships(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<Book>()
			.HasMany(e => e.Authors)
			.WithMany(e => e.Books);
	}

	private static void SeedData(ModelBuilder modelBuilder)
	{
		var parser = new CsvParser("Data/books.csv");
		var library = parser.CreateAllRecords();

		var linkTableRecords = library.BookLinkAuthor
			.DistinctBy(ab => new { ab.BooksBookId, ab.AuthorsAuthorId })
			.Select(ab => new Dictionary<string, object>
			{
				{ "BooksId", ab.BooksBookId },
				{ "AuthorsId", ab.AuthorsAuthorId }
			})
			.ToArray();


		modelBuilder.Entity<Book>()
			.HasData(library.Books);

		modelBuilder.Entity<Author>()
			.HasData(library.Authors);

		modelBuilder.Entity<Book>()
			.HasMany(b => b.Authors)
			.WithMany(a => a.Books)
			.UsingEntity<Dictionary<string, object>>(
				"BookLinkAuthor",
				b => b.HasData(linkTableRecords)
			);
	}
}