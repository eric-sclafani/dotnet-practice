using BlogApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Data;

public class BlogPostContext : DbContext
{
	public DbSet<BlogPost> BlogPost { get; set; }

	public BlogPostContext(DbContextOptions<BlogPostContext> options) : base(options)
	{
	}
}