using System.ComponentModel.DataAnnotations;

namespace FinanceTracker.Models;

public class Budget
{
	public int Id { get; set; }
	[Display(Name = "Cash In")] public int CashIn { get; set; }
	[Display(Name = "Cash Out")] public double CashOut { get; set; }
	[Display(Name = "Disp. Income")] public double DisposableIncome { get; set; }
}