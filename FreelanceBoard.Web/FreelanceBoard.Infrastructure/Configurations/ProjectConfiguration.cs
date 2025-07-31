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

			builder.HasKey(p => p.Id);

			builder.Property(p => p.Title)
				   .IsRequired()
				   .HasMaxLength(100);

			builder.Property(p => p.Description)
				   .IsRequired();

			builder.Property(p => p.Attachments)
				   .HasMaxLength(500); 

			builder.Property(p => p.UserId)
				   .IsRequired();

			builder.HasOne(p => p.User)
				   .WithMany(u => u.Projects)
				   .HasForeignKey(p => p.UserId);
				   
		}

	}
}
