using System.ComponentModel.DataAnnotations.Schema;

namespace JobSearch.Models;

public class Applicant
{
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public int Id { get; set; }

	public string? FirstName { get; set; }
	public string? LastName { get; set; }
	public string? Email { get; set; }
	public string? Location { get; set; }

	public int UserId { get; set; }
	public User? User { get; set; }

	// skills
}