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

			builder.Property(p => p.Message)
				   .HasMaxLength(1000);

			builder.Property(p => p.Status)
				   .IsRequired()
				   .HasMaxLength(50);

			builder.Property(p => p.Price)
				   .IsRequired()
				   .HasColumnType("decimal(18,2)");
		}
	}
}
