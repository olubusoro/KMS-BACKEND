using CsKmsBackend.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace CsKmsBackend.Infrastructure.Data
{
	public class KmsDbContext(DbContextOptions<KmsDbContext> options):DbContext(options) 
	{
		public DbSet<User> Users {  get; set; }
	}
}
