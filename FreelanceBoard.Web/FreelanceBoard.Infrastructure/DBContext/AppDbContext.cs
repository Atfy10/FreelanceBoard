using FreelanceBoard.Core.Domain;
using FreelanceBoard.Core.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace FreelanceBoard.Infrastructure.DBContext
{
    public class AppDbContext : IdentityDbContext <ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            base.OnConfiguring(options);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Contract>()
        .HasOne(c => c.Job)
        .WithOne(j => j.Contract)
        .HasForeignKey<Contract>(c => c.JobId)
        .OnDelete(DeleteBehavior.Restrict); // Prevent cascade delete

            builder.Entity<Proposal>()
        .HasOne(p => p.Job)
        .WithMany(j => j.Proposals)
        .HasForeignKey(p => p.JobId)
        .OnDelete(DeleteBehavior.Restrict); // 🛠 Prevent cascade

			builder.Entity<UserSkill>()
		.HasKey(us => new {
			us.UserId,
			us.SkillId
		});
			builder.Entity<UserSkill>()
			.HasOne(us => us.User)
			.WithMany(u => u.UserSkills)
			.HasForeignKey(us => us.UserId);

			builder.Entity<UserSkill>()
			.HasOne(us => us.Skill)
			.WithMany(s => s.UserSkills)
			.HasForeignKey(us => us.SkillId);

			builder.Entity<JobSkill>()
				.HasKey(js => new {
					js.JobId,
					js.SkillId
				});

			builder.Entity<JobSkill>()
				.HasOne(js => js.Job)
				.WithMany(j => j.JobSkills)
				.HasForeignKey(js => js.JobId);

			builder.Entity<JobSkill>()
				.HasOne(js => js.Skill)
				.WithMany(s => s.JobSkills)
				.HasForeignKey(js => js.SkillId);


		}


		public DbSet<Contract> Contracts { get; set; }
        public DbSet<Job> Jobs{ get; set; }
        public DbSet<Message> Messages{ get; set; }
        public DbSet<Notification> Notifications{ get; set; }
        public DbSet<Payement> Payements{ get; set; }
        public DbSet<Profile> Profiles{ get; set; }
        public DbSet<Project> Projects{ get; set; }
        public DbSet<Proposal> Proposals{ get; set; }
        public DbSet<Review> Reviews{ get; set; }
        public DbSet<Skill> Skills { get; set; }
        
    }
}
