using CsKmsBackend.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CsKmsBackend.Infrastructure.Data.Config
{
	public class AccessRequestConfiguration : IEntityTypeConfiguration<AccessRequest>
	{
		public void Configure(EntityTypeBuilder<AccessRequest> builder)
		{
			// Configure Class to store the Status enum as a string
			builder.Property(e => e.Status).HasConversion<string>();
		}
	}
}
