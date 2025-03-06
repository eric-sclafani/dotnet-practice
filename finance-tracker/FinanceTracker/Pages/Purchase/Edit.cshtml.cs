using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FinanceTracker.Models;
using FinanceTracker.Services;

namespace FinanceTracker.Pages.Purchase
{
	public class EditModel : PageModel
	{
		private readonly FinanceContext _context;
		[BindProperty] public Models.Purchase Purchase { get; set; } = default!;

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

			var purchase = await _context.Purchases.FirstOrDefaultAsync(m => m.Id == id);
			if (purchase == null)
			{
				return NotFound();
			}

			Purchase = purchase;
			return Page();
		}

		public async Task<IActionResult> OnPostAsync()
		{
			if (!ModelState.IsValid)
			{
				return Page();
			}

			_context.Attach(Purchase).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!PurchaseExists(Purchase.Id))
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

		private bool PurchaseExists(int id)
		{
			return _context.Purchases.Any(e => e.Id == id);
		}
	}
}