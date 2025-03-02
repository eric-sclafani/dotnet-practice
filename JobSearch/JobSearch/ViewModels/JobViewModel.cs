using JobSearch.Models;

namespace JobSearch.ViewModels;

public class JobViewModel
{
	public required int JobId { get; set; }
	public required string Title { get; set; }
	public required string Description { get; set; }
	public required string Employer { get; set; }
	public string Location { get; set; } = string.Empty;
	public string JobType { get; set; } = string.Empty;
	public string WorkMode { get; set; } = string.Empty;
	public int MinSalary { get; set; }
	public int MaxSalary { get; set; }
	public string PaySpecification { get; set; } = string.Empty;
}