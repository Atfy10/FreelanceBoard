using FreelanceBoard.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelanceBoard.Infrastructure.Configurations
{
    public class CategoryConfiguration
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            // Table name
            builder.ToTable("Categories");

            // Primary Key
            builder.HasKey(c => c.Id);

            // Category Name
            builder.Property(c => c.Name)
                   .IsRequired()
                   .HasMaxLength(100)
                   .HasColumnName("Name")
                   .HasComment("Name of the category");

            // Index for faster search
            builder.HasIndex(c => c.Name)
                   .IsUnique()
                   .HasDatabaseName("IX_Categories_Name");

            // Many-to-Many relationship
            builder.HasMany(c => c.CategoryJobs)
                .WithOne(j => j.Category);

            //builder.HasMany(c => c.Jobs)
            //       .WithMany(j => j.Categories)
            //       .UsingEntity<Dictionary<string, object>>(
            //            "JobCategories", // Join table name
            //            j => j.HasOne<Job>()
            //                  .WithMany()
            //                  .HasForeignKey("JobId")
            //                  .OnDelete(DeleteBehavior.Cascade),
            //            c => c.HasOne<Category>()
            //                  .WithMany()
            //                  .HasForeignKey("CategoryId")
            //                  .OnDelete(DeleteBehavior.Cascade),
            //            je =>
            //            {
            //                je.ToTable("JobCategories");
            //                je.HasKey("JobId", "CategoryId");
            //                je.HasIndex("CategoryId"); // Optional: for reverse lookups
            //            }
            //        );
        }
    }
}
