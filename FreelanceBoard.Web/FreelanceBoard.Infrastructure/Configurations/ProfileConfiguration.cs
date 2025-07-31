using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreelanceBoard.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FreelanceBoard.Infrastructure.Configurations
{
	internal class ProfileConfiguration : IEntityTypeConfiguration<Profile>
	{
		public void Configure(EntityTypeBuilder<Profile> builder)
		{
			builder.ToTable("Profiles");

			builder.HasKey(p => p.UserId);

			builder.HasOne(p => p.User)
				   .WithOne(u => u.Profile)
				   .HasForeignKey<Profile>(p => p.UserId);

			builder.Property(p => p.Bio)
				   .HasMaxLength(1000);

			builder.Property(p => p.Image)
				   .HasMaxLength(255);
		}
	}
}
