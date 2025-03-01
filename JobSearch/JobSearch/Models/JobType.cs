using System.ComponentModel.DataAnnotations.Schema;

namespace JobSearch.Models;

public class JobType
{
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public int Id { get; set; }

	public string Type { get; set; } = string.Empty;
}