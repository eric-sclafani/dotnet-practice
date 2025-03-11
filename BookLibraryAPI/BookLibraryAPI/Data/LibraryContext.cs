using BookLibraryAPI.Models;
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
		SeedData(modelBuilder);
	}

	private static void EstablishRelationships(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<Book>()
			.HasMany(e => e.Authors)
			.WithMany(e => e.Books);
	}

	private static void SeedData(ModelBuilder modelBuilder)
	{
		
	}
}