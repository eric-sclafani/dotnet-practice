using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using JobSearch.Models;

namespace JobSearch.ViewModels;

public class JobViewModel
{
	public int JobId { get; set; }
	public string Title { get; set; } = string.Empty;
	public string Description { get; set; } = string.Empty;
	public string Employer { get; set; } = string.Empty;
	public string Location { get; set; } = string.Empty;
	public string PayType { get; set; } = string.Empty;
	[DisplayName("Job Type")] public int JobTypeId { get; set; }
	[DisplayName("Work Mode")] public int WorkModeId { get; set; }
	[DisplayName("Minimum Salary")] public int MinSalary { get; set; }
	[DisplayName("Maximum Salary")] public int MaxSalary { get; set; }

	public IEnumerable<JobType>? JobTypeOptions { get; set; }
	public IEnumerable<WorkMode>? WorkModeOptions { get; set; }
}