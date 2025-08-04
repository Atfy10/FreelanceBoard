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
	internal class NotificationConfiguration : IEntityTypeConfiguration<Notification>
	{
		public void Configure(EntityTypeBuilder<Notification> builder)
		{
			builder.ToTable("Notifications");

			builder.HasKey(n => n.Id);

			builder.Property(n => n.Body)
				   .IsRequired()
				   .HasMaxLength(500);

			builder.Property(n => n.IsRead)
				   .HasDefaultValue(false);

			builder.Property(n => n.CreatedAt)
				   .IsRequired();

			builder.Property(n => n.UserId)
				   .IsRequired();

			builder.HasOne(n => n.User)
				   .WithMany(u => u.Notifications)
				   .HasForeignKey(n => n.UserId)
				   .OnDelete(DeleteBehavior.Cascade);
        }
	}
}
