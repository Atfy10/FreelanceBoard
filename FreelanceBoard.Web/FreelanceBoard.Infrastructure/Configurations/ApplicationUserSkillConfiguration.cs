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
	public class ApplicationUserSkillConfiguration : IEntityTypeConfiguration<ApplicationUserSkill>
	{
		public void Configure(EntityTypeBuilder<ApplicationUserSkill> builder)
		{
			builder.ToTable("ApplicationUserSkill");

			builder.HasKey(e => new { e.ApplicationUsersId, e.SkillsId });


			builder.HasOne(e => e.ApplicationUser)
				   .WithMany(u => u.UserSkills)
				   .HasForeignKey(e => e.ApplicationUsersId);

			builder.HasOne(e => e.Skill)
				   .WithMany(s => s.UserSkills)
				   .HasForeignKey(e => e.SkillsId);



		}
	}
}
