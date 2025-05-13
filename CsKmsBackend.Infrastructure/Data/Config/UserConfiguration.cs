using CsKmsBackend.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace CsKmsBackend.Infrastructure.Data.Config
{
	public class UserConfiguration : IEntityTypeConfiguration<User>
	{
		public void Configure(EntityTypeBuilder<User> builder)
		{
			// Configure MyClass to store the Role enum as a string

			builder.Property(e => e.Role).HasConversion<string>();


		}
	}
}
