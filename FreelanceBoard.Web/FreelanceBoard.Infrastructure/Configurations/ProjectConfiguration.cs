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
	internal class ProjectConfiguration : IEntityTypeConfiguration<Project>
	{
		public void Configure(EntityTypeBuilder<Project> builder)
		{
			builder.ToTable("Projects");

			builder.Property(p => p.Title)
				   .IsRequired()
				   .HasMaxLength(100);

			builder.Property(p => p.Description)
				   .HasMaxLength(1000);

			builder.Property(p => p.Attachments)
				   .HasMaxLength(500);
		}

	}
}
