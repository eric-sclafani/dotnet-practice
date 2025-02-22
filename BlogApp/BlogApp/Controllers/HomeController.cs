using System.Diagnostics;
using BlogApp.Data;
using Microsoft.AspNetCore.Mvc;
using BlogApp.Models;

namespace BlogApp.Controllers;

public class HomeController : Controller
{
	private readonly ILogger<HomeController> _logger;
	private readonly BlogPostContext _context;

	public HomeController(ILogger<HomeController> logger, BlogPostContext context)
	{
		_logger = logger;
		_context = context;
	}

	public IActionResult Index()
	{
		var posts = _context.BlogPost.OrderByDescending(p => p.CreatedAt).ToList();
		return View(posts);
	}
	
	[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
	public IActionResult Error()
	{
		return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
	}
	
	public IActionResult PageNotFound()
	{
		return View();
	}
}