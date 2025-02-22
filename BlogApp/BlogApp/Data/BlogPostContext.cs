using BlogApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Data;

public class BlogPostContext : DbContext
{
	public DbSet<BlogPost> BlogPost { get; set; }

	public BlogPostContext(DbContextOptions<BlogPostContext> options) : base(options)
	{
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		// seeds the BlogPost table with initial entries on model creation
		modelBuilder.Entity<BlogPost>().HasData(
			new BlogPost
			{
				Id = -1,
				Title = "This is test post #1",
				Content = "This is the content for test post #1. Wow, look at all of that content."
			},
			new BlogPost
			{
				Id = -2,
				Title = "This is test post #2",
				Content = "This is the content for test post #2. Wow, look at all of that content."
			}
		);
	}
}