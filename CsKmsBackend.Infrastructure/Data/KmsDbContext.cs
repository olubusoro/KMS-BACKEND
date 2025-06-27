using CsKmsBackend.Domain.Models;
using CsKmsBackend.Infrastructure.Data.Config;
using Microsoft.EntityFrameworkCore;

namespace CsKmsBackend.Infrastructure.Data
{
	public class KmsDbContext(DbContextOptions<KmsDbContext> options):DbContext(options) 
	{
		public DbSet<User> Users {  get; set; }
		public DbSet<Post> Posts { get; set; }
		public DbSet<Category> Categories { get; set;  }
		public DbSet<PostAttachment> PostAttachments { get; set; }
		public DbSet<AccessRequest> AccessRequests { get; set; }
		public DbSet<Department> Departments { get; set; }
		public DbSet<Log> Logs { get; set; }
		public DbSet<Notification> Notifications { get; set; }
		public DbSet<Feedback> Feedbacks { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			// Applies all configurations from the assembly of the specified type (e.g., BookConfiguration)
			modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserConfiguration).Assembly);

		}
	}
}
