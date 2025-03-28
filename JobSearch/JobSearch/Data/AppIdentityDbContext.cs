using JobSearch.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

using Microsoft.EntityFrameworkCore;

namespace JobSearch.Data;

public class AppIdentityDbContext:IdentityDbContext<AppUser>
{
	public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options) : base(options) { }
}