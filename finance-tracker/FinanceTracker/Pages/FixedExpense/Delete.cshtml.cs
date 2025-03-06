using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FinanceTracker.Models;
using FinanceTracker.Services;

namespace FinanceTracker.Pages.FixedExpense
{
	public class DeleteModel : PageModel
	{
		private readonly FinanceContext _context;
		[BindProperty] public Models.FixedExpense FixedExpense { get; set; } = default!;

		public DeleteModel(FinanceContext context)
		{
			_context = context;
		}

		public async Task<IActionResult> OnGetAsync(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var fixedexpense = await _context.FixedExpenses.FirstOrDefaultAsync(m => m.Id == id);

			if (fixedexpense is not null)
			{
				FixedExpense = fixedexpense;

				return Page();
			}

			return NotFound();
		}

		public async Task<IActionResult> OnPostAsync(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var fixedexpense = await _context.FixedExpenses.FindAsync(id);
			if (fixedexpense != null)
			{
				FixedExpense = fixedexpense;
				_context.FixedExpenses.Remove(FixedExpense);
				await _context.SaveChangesAsync();
			}

			return RedirectToPage("../Index");
		}
	}
}