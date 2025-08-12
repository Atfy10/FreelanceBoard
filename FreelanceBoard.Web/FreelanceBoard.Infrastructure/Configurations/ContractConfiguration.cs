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
            // Table Name
            builder.ToTable("Contracts");

            // Primary Key
            builder.HasKey(c => c.Id);

            // Dates
            builder.Property(c => c.StartDate)
                   .IsRequired()
                   .HasColumnName("StartDate")
                   .HasComment("Contract start date");

            builder.Property(c => c.EndDate)
                   .IsRequired()
                   .HasColumnName("EndDate")
                   .HasComment("Contract end date");

            // Price
            builder.Property(c => c.Price)
                   .HasColumnType("decimal(18,2)")
                   .IsRequired()
                   .HasComment("Total contract price");

            // Status
            builder.Property(c => c.Status)
                   .IsRequired()
                   .HasComment("Current contract status");
            // If enum: .HasConversion<string>().HasMaxLength(20)

            // Foreign Keys
            builder.Property(c => c.JobId)
                   .IsRequired()
                   .HasComment("Linked Job ID");

            builder.Property(c => c.UserId)
                   .IsRequired()
                   .HasComment("Linked User ID");

            builder.Property(c => c.PaymentNumber)
                   .IsRequired(false) // Nullable since OnDelete(SetNull)
                   .HasComment("Payment reference number");

            // Indexes for faster lookups
            builder.HasIndex(c => c.UserId).HasDatabaseName("IX_Contracts_UserId");
            builder.HasIndex(c => c.JobId).HasDatabaseName("IX_Contracts_JobId");
            builder.HasIndex(c => c.PaymentNumber).HasDatabaseName("IX_Contracts_PaymentNumber");

            // Relationships
            builder.HasOne(c => c.User)
                   .WithMany(u => u.Contracts)
                   .HasForeignKey(c => c.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(c => c.Job)
                   .WithOne(j => j.Contract)
                   .HasForeignKey<Contract>(c => c.JobId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(c => c.Payment)
                   .WithOne(p => p.Contract)
                   .HasForeignKey<Contract>(c => c.PaymentNumber)
                   .OnDelete(DeleteBehavior.SetNull);
        }
    }
}

