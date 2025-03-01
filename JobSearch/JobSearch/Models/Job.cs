using System.ComponentModel.DataAnnotations.Schema;

namespace JobSearch.Models;

public class Job
{
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public int Id { get; set; }

	public string? Title { get; set; }
	public string? Description { get; set; }
	public string? Employer { get; set; }
	public string? Location { get; set; }
	public int Salary { get; set; }
	
	public JobType JobType { get; set; } = null!;
	public int JobTypeId { get; set; }
	
	public WorkMode? WorkMode { get; set; }
	public int WorkModeId { get; set; }

	// schedule, benefits, responsibilities, required skills,
}