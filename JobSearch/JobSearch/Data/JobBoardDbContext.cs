using Microsoft.EntityFrameworkCore;

namespace JobSearch.Data;

public class JobBoardDbContext : DbContext
{
	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
	}
}