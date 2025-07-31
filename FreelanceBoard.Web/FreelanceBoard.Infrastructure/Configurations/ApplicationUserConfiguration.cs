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
	internal class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
	{
		public void Configure(EntityTypeBuilder<ApplicationUser> builder)
		{
			builder.ToTable("Users");

			builder.Property(u => u.FirstName)
				   .IsRequired()
				   .HasMaxLength(60);

			builder.Property(u => u.LastName)
				   .IsRequired()
				   .HasMaxLength(60);

			builder.Property(u => u.Email)
				   .IsRequired()
				   .HasMaxLength(255);

			builder.HasIndex(u => u.Email).IsUnique();

			builder.Property(u => u.IsBanned)
				   .HasDefaultValue(false);
		}
	}
}
