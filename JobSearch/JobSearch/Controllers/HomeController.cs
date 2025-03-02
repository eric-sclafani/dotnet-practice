using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using JobSearch.Models;
using JobSearch.ViewModels;

namespace JobSearch.Controllers;

public class HomeController : Controller
{
	private readonly ILogger<HomeController> _logger;

	public HomeController(ILogger<HomeController> logger)
	{
		_logger = logger;
	}

	public ViewResult Index()
	{
		return View();
	}

	public ViewResult Privacy()
	{
		return View();
	}

	public ViewResult PageNotFOund()
	{
		return View();
	}

	[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
	public IActionResult Error()
	{
		return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
	}
}