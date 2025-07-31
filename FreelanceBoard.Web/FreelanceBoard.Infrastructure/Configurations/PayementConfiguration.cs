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
	internal class PayementConfiguration : IEntityTypeConfiguration<Payement>
	{
		public void Configure(EntityTypeBuilder<Payement> builder)
		{
			builder.ToTable("Payements");

			builder.HasKey(p => p.PaymentNumber);

			builder.Property(p => p.Amount)
				   .HasColumnType("decimal(18,2)")
				   .IsRequired();

			builder.Property(p => p.Status)
				   .HasMaxLength(50)
				   .IsRequired();

			builder.Property(p => p.Date)
				   .IsRequired();
		}
	}
}
