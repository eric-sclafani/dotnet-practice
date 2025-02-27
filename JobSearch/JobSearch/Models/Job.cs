using System.ComponentModel.DataAnnotations.Schema;
using JobSearch.Enums;

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
	[Column(TypeName = "nvarchar(24)")] public JobType JobType { get; set; }
	[Column(TypeName = "nvarchar(24)")] public WorkMode WorkMode { get; set; }

	// schedule, benefits, responsibilities, required skills,
}