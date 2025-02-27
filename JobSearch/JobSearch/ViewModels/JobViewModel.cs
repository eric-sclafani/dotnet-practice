using JobSearch.Enums;

namespace JobSearch.ViewModels;

public class JobViewModel
{
	public string? Title { get; set; }
	public string? Description { get; set; }
	public string? Employer { get; set; }
	public string? Location { get; set; }
	public int Salary { get; set; }
	public JobType JobType { get; set; }
	public WorkMode WorkMode { get; set; }
}