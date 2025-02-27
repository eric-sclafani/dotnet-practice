using Microsoft.AspNetCore.Mvc;

namespace JobSearch.Controllers;

public class JobController : Controller
{
	public IActionResult Index()
	{
		return View();
	}
}