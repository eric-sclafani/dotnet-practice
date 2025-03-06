using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FinanceTracker.Models;
using FinanceTracker.Services;

namespace FinanceTracker.Pages.Purchase
{
	public class DeleteModel : PageModel
	{
		private readonly FinanceContext _context;
		[BindProperty] public Models.Purchase Purchase { get; set; } = default!;

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

			var purchase = await _context.Purchases.FirstOrDefaultAsync(m => m.Id == id);

			if (purchase is not null)
			{
				Purchase = purchase;

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

			var purchase = await _context.Purchases.FindAsync(id);
			if (purchase != null)
			{
				Purchase = purchase;
				_context.Purchases.Remove(Purchase);
				await _context.SaveChangesAsync();
			}

			return RedirectToPage("../Index");
		}
	}
}