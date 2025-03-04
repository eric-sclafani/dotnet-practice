using System.ComponentModel.DataAnnotations.Schema;

namespace JobSearch.Models;

public class Job
{
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public int Id { get; set; }

	public required string Title { get; set; }
	public required string Description { get; set; } 
	public required string Employer { get; set; }
	public string Location { get; set; } = string.Empty;
	public int MinSalary { get; set; }
	public int MaxSalary { get; set;  }
	public string PayType { get; set; } = string.Empty;

	public JobType JobType { get; set; } = null!;
	public int JobTypeId { get; set; }

	public WorkMode WorkMode { get; set; } = null!;
	public int WorkModeId { get; set; }

	// schedule, benefits, responsibilities, required skills,
}