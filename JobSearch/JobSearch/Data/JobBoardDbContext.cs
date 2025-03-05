using JobSearch.Models;
using Microsoft.EntityFrameworkCore;

namespace JobSearch.Data;

public class JobBoardDbContext : DbContext
{
	public DbSet<Job> Jobs { get; init; }
	public DbSet<JobType> JobTypes { get; init; }
	public DbSet<WorkMode> WorkModes { get; init; }
	public DbSet<Applicant> Applicants { get; init; }

	public JobBoardDbContext(DbContextOptions<JobBoardDbContext> options) : base(options)
	{
	}

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		optionsBuilder.UseSqlite("Data Source=JobSearch.db");
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		EstablishRelationships(modelBuilder);
		SeedData(modelBuilder);
	}

	private static void EstablishRelationships(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<Job>()
			.HasOne(j => j.JobType)
			.WithMany()
			.HasForeignKey(j => j.JobTypeId)
			.OnDelete(DeleteBehavior.Restrict);

		modelBuilder.Entity<Job>()
			.HasOne(j => j.WorkMode)
			.WithMany()
			.HasForeignKey(j => j.WorkModeId)
			.OnDelete(DeleteBehavior.Restrict);
	}

	private static void SeedData(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<JobType>().HasData(
			new JobType
			{
				Id = 1,
				Type = "Full Time"
			},
			new JobType
			{
				Id = 2,
				Type = "Part Time"
			},
			new JobType
			{
				Id = 3,
				Type = "Contract"
			}
		);

		modelBuilder.Entity<WorkMode>().HasData(
			new WorkMode
			{
				Id = 1,
				Mode = "Remote"
			},
			new WorkMode
			{
				Id = 2,
				Mode = "Hybrid"
			},
			new WorkMode
			{
				Id = 3,
				Mode = "In Person"
			}
		);


		modelBuilder.Entity<Job>().HasData(
			new Job
			{
				Id = -1,
				Title = "Junior .NET Developer",
				Description =
					"ABC Solutions is a Las Vegas-based software company specializing in custom enterprise solutions for businesses in the finance and healthcare industries. We develop scalable web applications using .NET technologies, helping our clients streamline operations and improve efficiency.\n\nWe’re looking for a Junior .NET Developer to join our growing team. In this role, you’ll work alongside experienced developers to build and maintain ASP.NET Core web applications, implement REST APIs, and optimize database queries using Entity Framework Core. You’ll also have the opportunity to participate in code reviews, collaborate on Agile development sprints, and gain hands-on experience with cloud deployment.",
				Employer = "ABC Solutions",
				JobTypeId = 1,
				WorkModeId = 1
			},
			new Job
			{
				Id = -2,
				Title = "Database Administrator",
				Description =
					"The Law Office of Shlifer & Launis is a well-established legal firm specializing in corporate law and litigation. We handle sensitive client data and require a skilled Database Administrator to ensure our systems remain secure, efficient, and compliant with industry regulations.\n\nAs a Database Administrator, you will be responsible for maintaining, optimizing, and securing our SQL databases, ensuring seamless access to case files, legal documents, and client records. You’ll also collaborate with our IT and legal teams to implement data backup strategies and improve database performance.",
				Employer = "The Law Office of Shlifer & Launis ",
				JobTypeId = 1,
				WorkModeId = 2
			},
			new Job
			{
				Id = -3,
				Title = "Executive Chef",
				Description =
					"Dumbo Steakhouse is a high-end dining establishment in the heart of Brooklyn, known for its premium cuts, aged steaks, and gourmet dining experience. We are seeking an Executive Chef to lead our kitchen, craft innovative dishes, and maintain the highest culinary standards.\n\nIn this role, you will oversee menu development, kitchen staff training, ingredient sourcing, and quality control. You’ll also be responsible for ensuring consistency, food safety compliance, and cost control while delivering an unforgettable dining experience.",
				Employer = "Dumbo Steakhouse",
				JobTypeId = 1,
				WorkModeId = 3
			},
			new Job
			{
				Id = -4,
				Title = "Delivery Driver",
				Description =
					"Southbound Deliveries is a regional logistics company specializing in fast and reliable last-mile deliveries. We are looking for a part-time Delivery Driver to help transport packages to businesses and residential customers.\n\nAs a driver, you will be responsible for safely operating company vehicles, ensuring on-time deliveries, and providing excellent customer service. The role requires a valid driver’s license, the ability to lift packages, and a strong attention to detail.",
				Employer = "Southbound Deliveries",
				JobTypeId = 3,
				WorkModeId = 3
			},
			new Job
			{
				Id = -5,
				Title = "Cashier",
				Description =
					"FoodMarket is a local grocery store committed to providing fresh, affordable food to the Hoboken community. We are hiring a part-time Cashier to assist customers, handle transactions, and ensure a smooth checkout experience.\n\nResponsibilities include scanning items, processing payments, providing friendly service, and assisting with store organization. This role is ideal for someone with strong communication skills and the ability to work in a fast-paced retail environment.",
				Employer = "FoodMarket",
				JobTypeId = 2,
				WorkModeId = 3
			},
			new Job
			{
				Id = -6,
				Title = "Fullstack Web Developer",
				Description =
					"Ajax Technologies is an innovative software company specializing in modern web applications for startups and enterprises. We are seeking a Fullstack Web Developer with expertise in the MERN stack (MongoDB, Express.js, React, Node.js) to help build and scale our next-generation digital platforms.\n\nIn this role, you will develop responsive front-end interfaces using React, design RESTful APIs with Node.js and Express, and manage MongoDB databases for efficient data handling. You’ll also work on state management, authentication systems, and cloud-based deployments.",
				Employer = "Ajax Technologies",
				JobTypeId = 1,
				WorkModeId = 2
			}
		);
	}
}