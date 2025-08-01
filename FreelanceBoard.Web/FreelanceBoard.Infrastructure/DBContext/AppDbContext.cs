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
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

			builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);


			// Apply Restrict globally on all FK relationships
			foreach (var entityType in builder.Model.GetEntityTypes())
			{
				foreach (var foreignKey in entityType.GetForeignKeys())
				{
					foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
				}
			}

			// Prevent cascading deletes
			builder.Entity<Contract>()
                .HasOne(c => c.Job)
                .WithOne(j => j.Contract)
                .HasForeignKey<Contract>(c => c.JobId)
                .OnDelete(DeleteBehavior.Restrict);

            // Prevent cascading deletes
            builder.Entity<Proposal>()
                .HasOne(p => p.Job)
                .WithMany(j => j.Proposals)
                .HasForeignKey(p => p.JobId)
                .OnDelete(DeleteBehavior.Restrict);

            // Avoid confusion in references
            builder.Entity<Message>()
                .HasOne(m => m.Sender)
                .WithMany(u => u.SentMessages)
                .HasForeignKey(m => m.SenderId)
                .OnDelete(DeleteBehavior.Restrict);

            // Avoid confusion in references
            builder.Entity<Message>()
                .HasOne(m => m.Receiver)
                .WithMany(u => u.ReceivedMessages)
                .HasForeignKey(m => m.ReceiverId)
                .OnDelete(DeleteBehavior.Restrict);

            // Profile.User relationship
            //builder.Entity<Profile>()
            //    .HasKey(p => p.Id);

            //builder.Entity<Profile>()
            //    .HasOne(p => p.User)
            //    .WithOne(u => u.Profile)
            //    .HasForeignKey<Profile>(p => p.Id);

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
