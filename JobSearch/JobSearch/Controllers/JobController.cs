using JobSearch.Data;
using Microsoft.AspNetCore.Mvc;

namespace JobSearch.Controllers;

public class JobController : Controller
{
	private readonly JobBoardDbContext _context;

	public JobController(JobBoardDbContext context)
	{
		_context = context;
	}

	public IActionResult Index()
	{
		return View();
	}

	public IActionResult List()
	{
		var jobs = _context.Jobs.ToList();
		return View(jobs);
	}
}