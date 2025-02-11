using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Todo;

public class TodoContext : DbContext
{
	public DbSet<TodoItem> Todos { get; set; }
	private string DbPath { get; } = Path.Combine(Directory.GetCurrentDirectory(), "todos.db");

	protected override void OnConfiguring(DbContextOptionsBuilder options)
		=> options.UseSqlite($"Data Source={DbPath}");
}

public class TodoItem
{
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public int Id { get; init; }

	public string? Text { get; set; }
	public bool IsCompleted { get; set; }
	public DateTime? DueDate { get; set; }
}