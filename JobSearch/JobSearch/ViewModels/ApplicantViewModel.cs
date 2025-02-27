using System.ComponentModel.DataAnnotations;

namespace JobSearch.ViewModels;

public class ApplicantViewModel
{
	[Required] public string? FirstName { get; set; }
	[Required] public string? LastName { get; set; }
	[Required] public string? Email { get; set; }
	[Required] public string? Location { get; set; }
}