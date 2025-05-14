using CsKmsBackend.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CsKmsBackend.Infrastructure.Data.Config
{
	public class UserConfiguration : IEntityTypeConfiguration<User>
	{
		public void Configure(EntityTypeBuilder<User> builder)
		{
			// Configure MyClass to store the Role enum as a string

			builder.Property(e => e.Role).HasConversion<string>();

			//builder.HasData(new User
			//{
			//	Id = -1,
			//	Name = "Super Admin",
			//	Email = "superadmin@mail.com",
			//	Password = "$2a$12$Qjue2sisW4MbkPaVGzEWq.5a.GECRr7YODQ2mM1V7gH1PFrp6BH1i",
			//	Role = UserRole.SuperAdmin,
			//	CreatedAt = new DateTime(2025, 5, 13, 23, 55, 0)
			//});
		}
	}
}
