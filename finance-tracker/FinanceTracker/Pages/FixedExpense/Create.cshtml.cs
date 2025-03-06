using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FinanceTracker.Services;

namespace FinanceTracker.Pages.FixedExpense
{
	public class CreateModel : PageModel
	{
		private readonly FinanceContext _context;

		[BindProperty] public Models.FixedExpense FixedExpense { get; set; } = default!;

		public CreateModel(FinanceContext context)
		{
			_context = context;
		}

		public IActionResult OnGet()
		{
			return Page();
		}

		public async Task<IActionResult> OnPostAsync()
		{
			if (!ModelState.IsValid)
			{
				return Page();
			}

			_context.FixedExpenses.Add(FixedExpense);
			await _context.SaveChangesAsync();

			return RedirectToPage("../Index");
		}
	}
}