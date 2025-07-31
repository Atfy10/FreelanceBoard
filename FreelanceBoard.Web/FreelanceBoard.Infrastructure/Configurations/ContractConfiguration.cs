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
	internal class ContractConfiguration : IEntityTypeConfiguration<Contract>
	{
		public void Configure(EntityTypeBuilder<Contract> builder)
		{
			builder.ToTable("Contracts");

			builder.HasKey(c => c.Id);

			builder.Property(c => c.StartDate)
				   .IsRequired();

			builder.Property(c => c.EndDate)
				   .IsRequired();

			builder.Property(c => c.Price)
				   .HasColumnType("decimal(18,2)")
				   .IsRequired();

			builder.Property(c => c.Status)
				   .IsRequired();
				   

			builder.Property(c => c.UserId)
				   .IsRequired();

			builder.Property(c => c.JobId)
				   .IsRequired();

			builder.Property(c => c.PaymentNumber)
				   .IsRequired();

			builder.HasOne(c => c.User)
				   .WithMany(u => u.Contracts)
				   .HasForeignKey(c => c.UserId);

			builder.HasOne(c => c.Job)
				   .WithOne(j => j.Contract)
				   .HasForeignKey<Contract>(c => c.JobId);

			builder.HasOne(c => c.Payment)
				   .WithOne(p => p.Contract)
				   .HasForeignKey<Contract>(c => c.PaymentNumber);
		}
	}
}

