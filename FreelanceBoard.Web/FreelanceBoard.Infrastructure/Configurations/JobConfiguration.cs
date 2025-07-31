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
	internal class JobConfiguration : IEntityTypeConfiguration<Job>
	{
		public void Configure(EntityTypeBuilder<Job> builder)
		{
			builder.ToTable("Jobs");

			builder.Property(j => j.Title)
				   .IsRequired()
				   .HasMaxLength(100);

			builder.Property(j => j.Description)
				   .IsRequired()
				   .HasMaxLength(1000);

			builder.Property(j => j.Category)
				   .IsRequired()
				   .HasMaxLength(100);

			builder.Property(j => j.Price)
				   .HasColumnType("decimal(18,2)")
				   .IsRequired();
		}
	}
}
