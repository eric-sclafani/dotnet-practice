using System.ComponentModel.DataAnnotations.Schema;

namespace JobSearch.Models;

public class User
{
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public int Id { get; set; }
	
	public string Username { get; set; } = string.Empty;
	public string PasswordHash { get; set; } = string.Empty;
	public Applicant Applicant { get; set; } = null!;
}