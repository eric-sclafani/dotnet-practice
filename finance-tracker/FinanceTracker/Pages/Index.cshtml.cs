using FinanceTracker.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FinanceTracker.Pages;

public class IndexModel : PageModel
{
	private readonly ILogger<IndexModel> _logger;
	private readonly FinanceContext _context;

	public Models.Budget Budget { get; set; } = default!;
	public IList<Models.FixedExpense> FixedExpenses { get; set; } = [];
	public IList<Models.Purchase> Purchases { get; set; } = [];
	public double Balance { get; set; }

	public IndexModel(ILogger<IndexModel> logger, FinanceContext context)
	{
		_logger = logger;
		_context = context;
	}

	public void OnGet()
	{
		GetData();
		UpdateBudget();
		Balance = CalculateBalance();
	}


	private async void GetData()
	{
		FixedExpenses = await _context.FixedExpenses.ToListAsync();
		Purchases = await _context.Purchases.ToListAsync();
		Budget = await _context.Budget.FirstAsync();
	}

	private int CalculateCashOut()
	{
		var expenses = from expense in FixedExpenses select expense.Amount;
		var expensesSum = expenses.Sum();
		return expensesSum;
	}

	private int CalculateDispoIncome()
	{
		return Budget.CashIn - (int)Budget.CashOut;
	}

	private async void UpdateBudget()
	{
		var entity = _context.Budget.FirstOrDefault(item => item.Id == 1);
		if (entity != null)
		{
			entity.CashOut = CalculateCashOut();
			entity.DisposableIncome = CalculateDispoIncome();
			await _context.SaveChangesAsync();
		}
	}

	private double CalculateBalance()
	{
		double? result = null;
		var firstPurchase = Purchases.FirstOrDefault(item => item.Id == 1);
		if (firstPurchase != null)
		{
			result = Budget.DisposableIncome - firstPurchase.Amount;
		}

		if (result != null)
		{
			var purchases = Purchases.Where(item => item.Id != 1);
			result = purchases.Aggregate(result, (current, purchase) => current - purchase.Amount);
		}


		return result ?? 0;
	}
}