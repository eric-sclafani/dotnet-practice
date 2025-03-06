using FinanceTracker.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinanceTracker.ViewComponents;

public class FixedExpenseViewComponent : ViewComponent
{
	private readonly FinanceContext _context;

	public FixedExpenseViewComponent(FinanceContext context)
	{
		_context = context;
	}
	public async Task<IViewComponentResult> InvokeAsync()
	{
		var fixedExpenses = await _context.FixedExpenses.ToListAsync();
		return View("Default",fixedExpenses);
	}
}