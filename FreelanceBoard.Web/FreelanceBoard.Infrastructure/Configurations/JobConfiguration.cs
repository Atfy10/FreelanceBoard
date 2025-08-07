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

			builder.HasKey(j => j.Id);

			builder.Property(j => j.Title)
				   .IsRequired()
				   .HasMaxLength(100);

			builder.Property(j => j.Description)
				   .IsRequired();



			builder.Property(j => j.Price)
				   .HasColumnType("decimal(18,2)")
				   .IsRequired();

			builder.Property(j => j.UserId)
				   .IsRequired();

			builder.HasOne(j => j.User)
				   .WithMany(u => u.Jobs)
				   .HasForeignKey(j => j.UserId)
				   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(j => j.Proposals)
				   .WithOne(p => p.Job)
				   .HasForeignKey(p => p.JobId)
				   .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(j => j.Skills)
				   .WithMany(s => s.Jobs)
				   .UsingEntity(j => j.ToTable("JobSkills")); 
		}
	}
}
