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

			builder.Property(c => c.Price)
				   .HasColumnType("decimal(18,2)");

			builder.Property(c => c.Status)
				   .HasConversion<string>()
				   .IsRequired();

			builder.Property(c => c.StartDate)
				   .IsRequired();

			builder.Property(c => c.EndDate)
				   .IsRequired();

			builder.Property(c => c.PayementNumber)
				   .HasMaxLength(50); 
		}
	}
}
