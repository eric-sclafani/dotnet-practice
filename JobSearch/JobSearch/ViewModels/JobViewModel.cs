using System.ComponentModel;
using JobSearch.Models;

namespace JobSearch.ViewModels;

public class JobViewModel
{
	public int JobId { get; set; }
	public string Title { get; set; } = string.Empty;
	public string Description { get; set; } = string.Empty;
	public string Employer { get; set; } = string.Empty;
	public string Location { get; set; } = string.Empty;
	[DisplayName("Job Type")] public int JobTypeId { get; set; }
	[DisplayName("Work Mode")] public int WorkModeId { get; set; }
	public int MinSalary { get; set; }
	public int MaxSalary { get; set; }
	public string PaySpecification { get; set; } = string.Empty;

	public IEnumerable<JobType> JobTypeOptions { get; set; } = null!;
	public IEnumerable<WorkMode> WorkModeOptions { get; set; } = null!;
}