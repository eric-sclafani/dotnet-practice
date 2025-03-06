using System.ComponentModel.DataAnnotations;

namespace FinanceTracker.Models;

public class FixedExpense
{
	public int Id { get; set; }
	public string? Category { get; set; }

	[Range(0, int.MaxValue, ErrorMessage = "Value must be greater than 0")]
	public int Amount { get; set; }
}