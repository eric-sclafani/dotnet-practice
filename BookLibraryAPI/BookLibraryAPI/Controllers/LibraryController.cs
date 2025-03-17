using BookLibraryAPI.Data;
using BookLibraryAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

	[HttpGet("/error")]
	public IActionResult Error()
	{
		return Problem();
	}

	[HttpGet]
	public IEnumerable<string> GetAllAuthors()
	{
		var authors = _context.Authors.Select(a => a.Name);
		return authors;
	}

	[HttpGet]
	public IEnumerable<string> GetAllBooks()
	{
		var books = _context.Books.Select(a => a.Title);
		return books;
	}

	[HttpGet]
	public IEnumerable<Book> GetBooksByAuthorId(int authorId)
	{
		var books = _context.Authors
			.Where(a => a.Id == authorId)
			.Include(a => a.Books)
			.SelectMany(a => a.Books);

		return books;
	}

	[HttpGet]
	public IEnumerable<Book> GetBooksByAuthorName(string name)
	{
		var cleanedName = name.ToLower().Trim();
		var books = _context.Authors
			.Where(a => a.Name.ToLower().Trim() == cleanedName)
			.Include(a => a.Books)
			.SelectMany(a => a.Books);

		return books;
	}

	[HttpGet]
	public IEnumerable<Book> GetBooksByRating(double threshold, string mode = "desc", int limit = 100)
	{
		var books = _context.Books
			.Where(b => b.AverageRating >= threshold)
			.Take(limit);

		books = mode switch
		{
			"desc" => books.OrderByDescending(b => b.AverageRating),
			"asc" => books.OrderBy(b => b.AverageRating),
			_ => books
		};

		return books;
	}
}