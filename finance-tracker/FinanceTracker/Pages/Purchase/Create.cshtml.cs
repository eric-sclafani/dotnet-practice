using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using FinanceTracker.Models;
using FinanceTracker.Services;

namespace FinanceTracker.Pages.Purchase
{
	public class CreateModel : PageModel
	{
		private readonly FinanceContext _context;
		[BindProperty] public Models.Purchase Purchase { get; set; } = default!;

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

			_context.Purchases.Add(Purchase);
			await _context.SaveChangesAsync();

			return RedirectToPage("../Index");
		}
	}
}