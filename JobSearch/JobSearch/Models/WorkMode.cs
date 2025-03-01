using System.ComponentModel.DataAnnotations.Schema;

namespace JobSearch.Models;

public class WorkMode
{
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public int Id { get; set; }

	public string Mode { get; set; } = string.Empty;
}