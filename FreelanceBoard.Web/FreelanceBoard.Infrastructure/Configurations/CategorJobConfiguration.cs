using FreelanceBoard.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelanceBoard.Infrastructure.Configurations
{
    public class CategorJobConfiguration : IEntityTypeConfiguration<CategoryJob>
    {
        public void Configure(EntityTypeBuilder<CategoryJob> builder)
        {
            builder.HasKey(cj => new { cj.JobsId, cj.CategoriesId });

            builder.HasOne(cj => cj.Job)
               .WithMany(j => j.CategoryJobs)
               .HasForeignKey(cj => cj.JobsId);

            builder.HasOne(cj => cj.Category)
                   .WithMany(c => c.CategoryJobs)
                   .HasForeignKey(cj => cj.CategoriesId);

        }
    }
}
