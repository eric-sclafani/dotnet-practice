using JobSearch.Data;
using JobSearch.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace JobSearch.Controllers;

public class JobController : Controller
{
	private readonly JobBoardDbContext _context;

	public JobController(JobBoardDbContext context)
	{
		_context = context;
	}

	public IActionResult Index(int id)
	{
		var job = _context.Jobs.FirstOrDefault(j => j.Id == id);
		if (job is null)
		{
			return RedirectToAction("PageNotFound", "Home");
		}

		// for job type and work mode, need to join on LU table 
		JobViewModel jobVm = new()
		{
			JobId = job.Id,
			Title = job.Title,
			Description = job.Description,
			Employer = job.Employer,
			Location = job.Location,
		};
		return View(jobVm);
	}

	public ViewResult List()
	{
		var jobs = _context.Jobs.ToList();
		return View(jobs);
	}

	public IActionResult AddEdit(int id)
	{
		if (id == 0)
		{
			ViewBag.IsEditing = false;
			return View();
		}

		var job = _context.Jobs.FirstOrDefault(j => j.Id == id);
		if (job is null)
		{
			return RedirectToAction("PageNotFound", "Home");
		}
		
		JobViewModel jobVm = new()
		{
			JobId = job.Id,
			Title = job.Title,
			Description = job.Description,
			Employer = job.Employer,
			Location = job.Location,
		};

		ViewBag.IsEditing = true;
		return View(jobVm);
	}

	[HttpPost]
	public IActionResult AddEdit(JobViewModel jobVmForm)
	{
		if (!ModelState.IsValid)
		{
			return View();
		}
		return View();
	}
}