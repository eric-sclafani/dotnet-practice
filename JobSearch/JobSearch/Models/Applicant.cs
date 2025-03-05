using System.ComponentModel.DataAnnotations.Schema;

namespace JobSearch.Models;

public class Applicant
{
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public int Id { get; set; }

	public string FirstName { get; set; } = string.Empty;
	public string LastName { get; set; } = string.Empty;
	public string Email { get; set; } = string.Empty;
	public string City { get; set; } = string.Empty;
	public string State { get; set; } = string.Empty;
	public string PhoneNumber { get; set; } = string.Empty;

	// skills
}