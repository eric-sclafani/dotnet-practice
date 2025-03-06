using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FinanceTracker.Services;

namespace FinanceTracker.Pages.FixedExpense
{
	public class EditModel : PageModel
	{
		private readonly FinanceContext _context;
		[BindProperty] public Models.FixedExpense FixedExpense { get; set; } = default!;

		public EditModel(FinanceContext context)
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
			if (fixedexpense == null)
			{
				return NotFound();
			}

			FixedExpense = fixedexpense;
			return Page();
		}

		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more information, see https://aka.ms/RazorPagesCRUD.
		public async Task<IActionResult> OnPostAsync()
		{
			if (!ModelState.IsValid)
			{
				return Page();
			}

			_context.Attach(FixedExpense).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!FixedExpenseExists(FixedExpense.Id))
				{
					return NotFound();
				}
				else
				{
					throw;
				}
			}

			return RedirectToPage("../Index");
		}

		private bool FixedExpenseExists(int id)
		{
			return _context.FixedExpenses.Any(e => e.Id == id);
		}
	}
}