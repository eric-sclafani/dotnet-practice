namespace TodoCLI.Models;

using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

public class TodoContext : DbContext
{
	public DbSet<Todo> Todos { get; set; }

	private string DbPath { get; } = Path.Combine(Directory.GetCurrentDirectory(), "todos.db");

	protected override void OnConfiguring(DbContextOptionsBuilder options)
		=> options.UseSqlite($"Data Source={DbPath}");
}

public class Todo
{
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public int Id { get; init; }

	public string? Description { get; set; }
	public string? Status { get; set; }
	public DateTime CreatedAt { get; set; }
	public DateTime UpdatedAt { get; set; }
}