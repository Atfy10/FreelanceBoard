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
	internal class ProposalConfiguration : IEntityTypeConfiguration<Proposal>
	{
		public void Configure(EntityTypeBuilder<Proposal> builder)
		{
			builder.ToTable("Proposals");

			builder.HasKey(p => p.Id);

			builder.Property(p => p.Message)
				   .IsRequired()
				   .HasMaxLength(1000);

			builder.Property(p => p.Status)
				   .IsRequired()
				   .HasMaxLength(50);

			builder.Property(p => p.Price)
				   .HasColumnType("decimal(18,2)")
				   .IsRequired();

			builder.Property(p => p.FreelancerId)
				   .IsRequired();

			builder.Property(p => p.JobId)
				   .IsRequired();

			builder.HasOne(p => p.Freelancer)
				   .WithMany(u => u.Proposals)
				   .HasForeignKey(p => p.FreelancerId);


			builder.HasOne(p => p.Job)
				   .WithMany(j => j.Proposals)
				   .HasForeignKey(p => p.JobId);
				   
		}
	}
}
