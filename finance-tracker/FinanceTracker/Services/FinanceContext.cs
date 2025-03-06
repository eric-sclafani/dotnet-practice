using FinanceTracker.Data.Configurations;
using FinanceTracker.Data;
using FinanceTracker.Models;
using Microsoft.EntityFrameworkCore;

namespace FinanceTracker.Services;

public class FinanceContext : DbContext
{
	public DbSet<FixedExpense> FixedExpenses { get; set; }
	public DbSet<Budget> Budget { get; set; }
	public DbSet<Purchase> Purchases { get; set; }

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		optionsBuilder.UseSqlite(@"Data source=Data/Finances.db");
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfiguration(new PurchaseConfiguration()).SeedPurchases();
		modelBuilder.ApplyConfiguration(new FixedExpenseConfiguration()).SeedFixedExpenses();
		modelBuilder.ApplyConfiguration(new BudgetConfiguration()).SeedBudget();
	}
}