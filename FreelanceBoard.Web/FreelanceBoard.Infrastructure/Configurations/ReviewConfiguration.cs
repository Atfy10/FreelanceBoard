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
	internal class ReviewConfiguration : IEntityTypeConfiguration<Review>
	{
		public void Configure(EntityTypeBuilder<Review> builder)
		{
			builder.ToTable("Reviews");

			builder.HasKey(r => r.Id);

			builder.Property(r => r.Rating)
				   .IsRequired();

			builder.Property(r => r.Feedback)
				   .HasMaxLength(1000); 

			builder.Property(r => r.Date)
				   .IsRequired();

			builder.Property(r => r.ContractId)
				   .IsRequired();

			builder.Property(r => r.ReviewerId)
				   .IsRequired();

			builder.HasOne(r => r.Contract)
				   .WithMany(c => c.Reviews)
				   .HasForeignKey(r => r.ContractId)
				   .OnDelete(DeleteBehavior.Restrict);

			builder.HasOne(r => r.Reviewer)
				   .WithMany()
				   .HasForeignKey(r => r.ReviewerId)
				   .OnDelete(DeleteBehavior.Restrict);

			//not sure if it will work [check]
			builder.HasCheckConstraint("CK_Review_Rating", "[Rating] >= 1 AND [Rating] <= 5"); 
		}
	}
}
