using JobSearch.Data;
using JobSearch.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

	public async Task<IActionResult> AddEdit(int id)
	{
		var jobTypes = await _context.JobTypes.ToListAsync();
		var workModes = await _context.WorkModes.ToListAsync();

		JobViewModel jobVm = new()
		{
			JobTypeOptions = jobTypes,
			WorkModeOptions = workModes
		};

		if (id == 0)
		{
			ViewBag.IsEditing = false;
			return View(jobVm);
		}

		var job = _context.Jobs.FirstOrDefault(j => j.Id == id);
		if (job is null)
		{
			return RedirectToAction("PageNotFound", "Home");
		}

		jobVm.JobId = job.Id;
		jobVm.Title = job.Title;
		jobVm.Description = job.Description;
		jobVm.Employer = job.Employer;
		jobVm.Location = job.Location;

		ViewBag.IsEditing = true;
		return View(jobVm);
	}

	[HttpPost]
	public IActionResult AddEdit(JobViewModel form)
	{
		if (!ModelState.IsValid)
		{
			return View();
		}

		
		
		return RedirectToAction("Index", new { id = form.JobId });
	}
}