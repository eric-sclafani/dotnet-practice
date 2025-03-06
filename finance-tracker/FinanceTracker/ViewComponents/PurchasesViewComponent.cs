using FinanceTracker.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinanceTracker.ViewComponents;

public class PurchasesViewComponent : ViewComponent
{
	private readonly FinanceContext _context;

	public PurchasesViewComponent(FinanceContext context)
	{
		_context = context;
	}

	public async Task<IViewComponentResult> InvokeAsync()
	{
		var purchases = await _context.Purchases.ToListAsync();
		return View("Default", purchases);
	}
}