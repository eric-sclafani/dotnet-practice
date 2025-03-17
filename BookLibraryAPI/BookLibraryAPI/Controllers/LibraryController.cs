using BookLibraryAPI.Data;
using BookLibraryAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookLibraryAPI.Controllers;

[ApiController]
[Route("api/lib/[action]")]
public class LibraryController : Controller
{
	private readonly LibraryContext _context;

	public LibraryController(LibraryContext context)
	{
		_context = context;
	}

	[HttpGet]
	public IEnumerable<string> GetAllAuthors()
	{
		var authors = _context.Authors.Select(a => a.Name);
		return authors;
	}

	[HttpGet("/error")]
	public IActionResult Error()
	{
		return Problem();
	}
}