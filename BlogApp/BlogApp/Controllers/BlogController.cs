using BlogApp.Data;
using BlogApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.Controllers;

public class BlogController : Controller
{
	private readonly BlogPostContext _context;

	public BlogController(BlogPostContext context)
	{
		_context = context;
	}

	public IActionResult Index(int id)
	{
		var post = _context.BlogPost.FirstOrDefault(p => p.Id == id);
		if (post == null)
			return NotFound();

		return View(post);
	}

	public IActionResult Create()
	{
		return View();
	}

	[HttpPost]
	public async Task<IActionResult> Create(BlogPost post)
	{
		if (ModelState.IsValid)
		{
			_context.BlogPost.Add(post);
			await _context.SaveChangesAsync();
			return RedirectToAction("Index", "Home");
		}

		return View(post);
	}

	public IActionResult Delete()
	{
		return View();
	}

	public IActionResult Update(int id)
	{
		var post = _context.BlogPost.FirstOrDefault(p => p.Id == id);
		if (post == null)
			return NotFound();

		return View(post);
	}

	// [HttpPost]
	// public async Task<IActionResult> Update(BlogPost post)
	// {
	// 	if (ModelState.IsValid)
	// 	{
	// 		
	// 	}
	// 	return View(post);
	// }
}