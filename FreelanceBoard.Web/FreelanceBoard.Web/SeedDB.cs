using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FreelanceBoard.Core.Domain.Entities;
using FreelanceBoard.Infrastructure.DBContext;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FreelanceBoard.Web
{
    public static class SeedDB
    {
        // Entry point - call this once on startup.
        public static async Task SeedAsync(AppDbContext context, UserManager<ApplicationUser> userManager)
        {
            // Order matters to satisfy FK constraints
            await SeedUsersAsync(context, userManager);
            await SeedSkillsAsync(context);
            await SeedCategoriesAsync(context);
            await SeedProfilesAsync(context);
            await SeedProjectsAsync(context);
            await SeedJobsAsync(context);
            await SeedProposalsAsync(context);
            await SeedPaymentsAsync(context);
            await SeedContractsAsync(context);
            await SeedReviewsAsync(context);
            await SeedMessagesAsync(context);
            await SeedNotificationsAsync(context);
        }

        // 1) USERS (10 distinct)
        private static async Task SeedUsersAsync(AppDbContext context, UserManager<ApplicationUser> userManager)
        {
            if (await context.Users.AnyAsync())
                return;

            var names = new (string First, string Last)[]
            {
                ("John","Doe"), ("Jane","Smith"), ("Mike","Johnson"), ("Sarah","Williams"),
                ("Alex","Brown"), ("Nora","Davis"), ("Omar","Hassan"), ("Lina","Khalil"),
                ("Youssef","Ibrahim"), ("Maya","Farouk")
            };

            for (int i = 0; i < names.Length; i++)
            {
                var (first, last) = names[i];
                var user = new ApplicationUser
                {
                    UserName = $"{first.ToLower()}{last.ToLower()}",
                    Email = $"{first.ToLower()}.{last.ToLower()}@seed.local",
                    FirstName = first,
                    LastName = last,
                    IsBanned = (i == names.Length - 1), // just 1 banned sample
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(user, "Password123!");
                if (!result.Succeeded)
                {
                    var msg = string.Join(", ", result.Errors.Select(e => e.Description));
                    throw new Exception($"Failed creating seed user {user.Email}: {msg}");
                }
            }
        }

        // 2) SKILLS (10 distinct)
        private static async Task SeedSkillsAsync(AppDbContext context)
        {
            if (await context.Skills.AnyAsync())
                return;

            var skillNames = new[]
            {
                "Web Development","Graphic Design","Mobile App Development","Content Writing","Digital Marketing",
                "Data Analysis","Video Editing","UI/UX Design","SEO Optimization","Cloud Engineering"
            };
            context.Skills.AddRange(skillNames.Select(n => new Skill { Name = n }));
            await context.SaveChangesAsync();
        }

        // 3) CATEGORIES (10 distinct)
        private static async Task SeedCategoriesAsync(AppDbContext context)
        {
            if (await context.Categories.AnyAsync())
                return;

            var categoryNames = new[]
            {
                "Web Development","Design & Creative","Mobile Development","Writing & Translation","Digital Marketing",
                "Data Science & Analytics","Video & Animation","DevOps","Cybersecurity","Project Management"
            };
            context.Categories.AddRange(categoryNames.Select(n => new Category { Name = n }));
            await context.SaveChangesAsync();
        }

        // 4) PROFILES (10; 1 per user)
        private static async Task SeedProfilesAsync(AppDbContext context)
        {
            if (await context.Profiles.AnyAsync())
                return;

            var users = await context.Users.OrderBy(u => u.Id).ToListAsync();
            var profiles = users.Take(10).Select((u, i) => new Profile
            {
                UserId = u.Id,
                Bio = $"Bio for {u.FirstName} {u.LastName} (#{i + 1}).",
                Image = $"profile_{i + 1}.jpg"
            });

            context.Profiles.AddRange(profiles);
            await context.SaveChangesAsync();
        }

        // 5) PROJECTS (10; linked to random users)
        private static async Task SeedProjectsAsync(AppDbContext context)
        {
            if (await context.Projects.AnyAsync())
                return;

            var rnd = new Random(123);
            var users = await context.Users.AsNoTracking().ToListAsync();

            var projects = Enumerable.Range(1, 10).Select(i => new Project
            {
                Title = $"Project #{i}",
                Description = $"Description for Project #{i}",
                Attachments = $"project_{i}.zip",
                UserId = users[rnd.Next(users.Count)].Id
            });

            context.Projects.AddRange(projects);
            await context.SaveChangesAsync();
        }

        // 6) JOBS (10; link existing Skills & Categories)
        private static async Task SeedJobsAsync(AppDbContext context)
        {
            if (await context.Jobs.AnyAsync())
                return;

            var rnd = new Random(456);
            var users = await context.Users.AsNoTracking().ToListAsync();
            var allSkills = await context.Skills.ToListAsync();
            var allCats = await context.Categories.ToListAsync();

            var jobs = new List<Job>();
            for (int i = 0; i < 10; i++)
            {
                var job = new Job
                {
                    Title = $"Job #{i + 1}",
                    Description = $"Detailed description for job #{i + 1}.",
                    Price = 200 + i * 100,
                    UserId = users[rnd.Next(users.Count)].Id,
                    Deadline = DateTime.Now.AddDays(10 + i),
                    DateCreated = DateTime.Now.AddDays(-i)
                };

                // Attach existing skills/categories (no duplicates)
                job.Skills = allSkills.OrderBy(_ => rnd.Next()).Take(2 + (i % 2)).ToList();
                var selectedCats = allCats
                    .OrderBy(_ => rnd.Next())
                    .Take(1 + (i % 2))
                    .ToList();

                foreach (var cat in selectedCats)
                {
                    job.CategoryJobs.Add(new CategoryJob
                    {
                        Category = cat,  // or CategoriesId = cat.Id if you want to set by ID
                        Job = job
                    });
                }

                jobs.Add(job);
            }

            context.Jobs.AddRange(jobs);
            await context.SaveChangesAsync();
        }

        // 7) PROPOSALS (10; link to Users+Jobs)
        private static async Task SeedProposalsAsync(AppDbContext context)
        {
            if (await context.Proposals.AnyAsync())
                return;

            var rnd = new Random(789);
            var jobs = await context.Jobs.AsNoTracking().ToListAsync();
            var users = await context.Users.AsNoTracking().ToListAsync();
            var statuses = new[] { "Pending", "Accepted", "Rejected", "Completed" };

            var proposals = Enumerable.Range(1, 10).Select(i => new Proposal
            {
                Message = $"Proposal message #{i}",
                Status = statuses[i % statuses.Length],
                Price = 300 + i * 75,
                FreelancerId = users[rnd.Next(users.Count)].Id,
                JobId = jobs[rnd.Next(jobs.Count)].Id
            });

            context.Proposals.AddRange(proposals);
            await context.SaveChangesAsync();
        }

        // 8) PAYEMENTS (10; PaymentNumber PK)
        private static async Task SeedPaymentsAsync(AppDbContext context)
        {
            if (await context.Payements.AnyAsync())
                return;

            var payments = Enumerable.Range(1, 10).Select(i => new Payement
            {
                PaymentNumber = $"PAY-{i:000}",
                Amount = 250 + i * 100,
                Status = (i % 3 == 0) ? "Pending" : "Paid",
                Date = DateTime.Now.AddDays(-i * 3)
            });

            context.Payements.AddRange(payments);
            await context.SaveChangesAsync();
        }

        // 9) CONTRACTS (10; 1-1 with Jobs; optional Payement)
        private static async Task SeedContractsAsync(AppDbContext context)
        {
            if (await context.Contracts.AnyAsync())
                return;

            var rnd = new Random(246);
            var jobs = await context.Jobs.AsNoTracking().OrderBy(j => j.Id).ToListAsync();
            var users = await context.Users.AsNoTracking().ToListAsync();
            var pays = await context.Payements.AsNoTracking().Select(p => p.PaymentNumber).ToListAsync();
            var statuses = new[] { "Active", "Completed", "Cancelled", "Pending" };

            // Ensure at most one contract per job (1-1)
            var contracts = new List<Contract>();
            for (int i = 0; i < Math.Min(10, jobs.Count); i++)
            {
                var start = DateTime.Now.AddDays(-30 - i);
                contracts.Add(new Contract
                {
                    StartDate = start,
                    EndDate = start.AddDays(30),
                    Price = 400 + i * 120,
                    Status = statuses[i % statuses.Length],
                    UserId = users[rnd.Next(users.Count)].Id,
                    JobId = jobs[i].Id,
                    // sometimes null to exercise SetNull behavior
                    PaymentNumber = (i % 2 == 0 && pays.Count > 0) ? pays[rnd.Next(pays.Count)] : null
                });
            }

            context.Contracts.AddRange(contracts);
            await context.SaveChangesAsync();
        }

        // 10) REVIEWS (10; link to Contract + Reviewer)
        private static async Task SeedReviewsAsync(AppDbContext context)
        {
            if (await context.Reviews.AnyAsync())
                return;

            var rnd = new Random(135);
            var contracts = await context.Contracts.AsNoTracking().ToListAsync();
            var users = await context.Users.AsNoTracking().ToListAsync();

            var reviews = Enumerable.Range(1, 10).Select(i => new Review
            {
                Rating = 3 + (i % 3), // 3..5
                Feedback = $"Feedback for contract #{i}",
                Date = DateTime.Now.AddDays(-(5 + i)),
                ContractId = contracts[rnd.Next(contracts.Count)].Id,
                ReviewerId = users[rnd.Next(users.Count)].Id
            });

            context.Reviews.AddRange(reviews);
            await context.SaveChangesAsync();
        }

        // 11) MESSAGES (10; Sender <> Receiver)
        private static async Task SeedMessagesAsync(AppDbContext context)
        {
            if (await context.Messages.AnyAsync())
                return;

            var rnd = new Random(975);
            var users = await context.Users.AsNoTracking().ToListAsync();
            var msgs = new List<Message>();

            for (int i = 1; i <= 10; i++)
            {
                var sender = users[rnd.Next(users.Count)];
                var receiver = users.Where(u => u.Id != sender.Id).OrderBy(_ => rnd.Next()).First();

                msgs.Add(new Message
                {
                    Body = $"Message body #{i}",
                    IsRead = (i % 2 == 0),
                    Timestamp = DateTime.Now.AddDays(-i),
                    SenderId = sender.Id,
                    ReceiverId = receiver.Id
                });
            }

            context.Messages.AddRange(msgs);
            await context.SaveChangesAsync();
        }

        // 12) NOTIFICATIONS (10; link to User)
        private static async Task SeedNotificationsAsync(AppDbContext context)
        {
            if (await context.Notifications.AnyAsync())
                return;

            var rnd = new Random(864);
            var users = await context.Users.AsNoTracking().ToListAsync();

            var notes = Enumerable.Range(1, 10).Select(i => new Notification
            {
                Body = $"Notification #{i}",
                IsRead = (i % 3 == 0),
                CreatedAt = DateTime.Now.AddDays(-(i + 1)),
                UserId = users[rnd.Next(users.Count)].Id
            });

            context.Notifications.AddRange(notes);
            await context.SaveChangesAsync();
        }
    }
}
