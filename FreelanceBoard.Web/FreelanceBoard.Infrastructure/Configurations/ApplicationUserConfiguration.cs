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
	internal class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
	{
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            // Table name
            builder.ToTable("Users");

            // First Name
            builder.Property(u => u.FirstName)
                   .IsRequired()
                   .HasMaxLength(60)
                   .HasColumnName("FirstName")
                   .HasComment("User's first name");

            // Last Name
            builder.Property(u => u.LastName)
                   .IsRequired()
                   .HasMaxLength(60)
                   .HasColumnName("LastName")
                   .HasComment("User's last name");

            // Email
            builder.Property(u => u.Email)
                   .IsRequired()
                   .HasMaxLength(255)
                   .HasColumnName("Email")
                   .HasComment("User's email address");

            builder.HasIndex(u => u.Email)
                   .IsUnique()
                   .HasDatabaseName("IX_Users_Email");

            // Phone Number
            builder.Property(u => u.PhoneNumber)
                   .HasMaxLength(20)
                   .HasColumnName("PhoneNumber")
                   .HasComment("User's phone number (optional)");

            builder.HasIndex(u => u.PhoneNumber)
                   .IsUnique()
                   .HasDatabaseName("IX_Users_PhoneNumber");

            // IsBanned
            builder.Property(u => u.IsBanned)
                   .HasDefaultValue(false)
                   .HasColumnName("IsBanned")
                   .HasComment("Indicates if the user is banned");
        }
	}
}
