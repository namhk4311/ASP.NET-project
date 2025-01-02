using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;


namespace WebApplication1.Services
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions options) : base(options)
		{

		}

		public DbSet<User> Users { get; set; }
		public DbSet<Course> Courses { get; set; }
		public DbSet<Class> Classes { get; set; }
		public DbSet<Registration> Registrations { get; set; }
	}
}
