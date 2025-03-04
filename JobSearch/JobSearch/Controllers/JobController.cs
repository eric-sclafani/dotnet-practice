using JobSearch.Data;
using JobSearch.Models;
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
		JobViewModel vm = new()
		{
			JobTypeOptions = await _context.JobTypes.ToListAsync(),
			WorkModeOptions = await _context.WorkModes.ToListAsync()
		};

		if (id == 0)
		{
			ViewBag.IsEditing = false;
			return View(vm);
		}

		var job = _context.Jobs.FirstOrDefault(j => j.Id == id);
		if (job is null)
		{
			return RedirectToAction("PageNotFound", "Home");
		}

		vm.JobId = job.Id;
		vm.Title = job.Title;
		vm.Description = job.Description;
		vm.Employer = job.Employer;
		vm.Location = job.Location;
		vm.MinSalary = job.MinSalary;
		vm.MaxSalary = job.MaxSalary;
		vm.PayType = job.PayType;
		

		ViewBag.IsEditing = true;
		return View(vm);
	}

	[HttpPost]
	public async Task<IActionResult> AddEdit(JobViewModel form)
	{
		var job = _context.Jobs.FirstOrDefault(j => j.Id == form.JobId);

		if (!ModelState.IsValid)
		{
			form.JobTypeOptions = await _context.JobTypes.ToListAsync();
			form.WorkModeOptions = await _context.WorkModes.ToListAsync();
			ViewBag.IsEditing = form.JobId != 0;
			return View(form);
		}

		if (job is null)
		{
			Job newJob = new()
			{
				Title = form.Title,
				Description = form.Description,
				Employer = form.Employer,
				Location = form.Location,
				PayType = form.PayType,
				WorkModeId = form.WorkModeId,
				JobTypeId = form.JobTypeId,
				MinSalary = form.MinSalary,
				MaxSalary = form.MaxSalary,
			};
			_context.Jobs.Add(newJob);
			await _context.SaveChangesAsync();
			return RedirectToAction("Index", new { id = newJob.Id });
		}

		job.Title = form.Title;
		job.Description = form.Description;
		job.Employer = form.Employer;
		job.Location = form.Location;
		job.PayType = form.PayType;
		job.WorkModeId = form.WorkModeId;
		job.JobTypeId = form.JobTypeId;
		job.MinSalary = form.MinSalary;
		job.MaxSalary = form.MaxSalary;
		await _context.SaveChangesAsync();

		return RedirectToAction("Index", new { id = form.JobId });
	}
}