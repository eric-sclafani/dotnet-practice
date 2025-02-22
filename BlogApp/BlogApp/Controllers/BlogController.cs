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

	public IActionResult AddOrEdit(int id)
	{
		if (id == 0)
		{
			ViewBag.Action = "Add";
			return View();
		}

		var post = _context.BlogPost.FirstOrDefault(p => p.Id == id);
		if (post == null)
			return NotFound();

		ViewBag.Action = "Edit";
		return View(post);
	}

	[HttpPost]
	public async Task<IActionResult> AddOrEdit(BlogPost form)
	{
		var post = _context.BlogPost.FirstOrDefault(p => p.Id == form.Id);

		if (ModelState.IsValid)
		{
			if (form.Id == 0)
			{
				_context.BlogPost.Add(form);
				await _context.SaveChangesAsync();
				return RedirectToAction("Index", new { id = form.Id });
			}

			post.Title = form.Title;
			post.Content = form.Content;
			post.LastUpdatedAt = DateTime.Now;
			await _context.SaveChangesAsync();
			return RedirectToAction("Index", new { id = form.Id });
		}

		return View(post);
	}

	public IActionResult Delete(int id)
	{
		var post = _context.BlogPost.FirstOrDefault(p => p.Id == id);
		if (post == null)
			return NotFound();

		return View(post);
	}

	[HttpPost]
	public async Task<IActionResult> Delete(BlogPost post)
	{
		_context.BlogPost.Remove(post);
		await _context.SaveChangesAsync();
		return RedirectToAction("Index", "Home");
	}
}