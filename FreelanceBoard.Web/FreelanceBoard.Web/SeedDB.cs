using FreelanceBoard.Core.Domain.Entities;
using FreelanceBoard.Infrastructure.DBContext;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FreelanceBoard.Web
{
    public class SeedDB
    {
        public static void Seed(AppDbContext context, UserManager<ApplicationUser> userManager)
        {
            if (!context.Users.Any())
            {
                var users = new List<ApplicationUser>
            {
                new ApplicationUser
                {
                    Id = "1",
                    UserName = "johndoe",
                    Email = "john.doe@example.com",
                    FirstName = "John",
                    LastName = "Doe",
                    IsBanned = false,
                    EmailConfirmed = true
                },
                new ApplicationUser
                {
                    Id = "2",
                    UserName = "janesmith",
                    Email = "jane.smith@example.com",
                    FirstName = "Jane",
                    LastName = "Smith",
                    IsBanned = false,
                    EmailConfirmed = true
                },
                new ApplicationUser
                {
                    Id = "3",
                    UserName = "mikejohnson",
                    Email = "mike.johnson@example.com",
                    FirstName = "Mike",
                    LastName = "Johnson",
                    IsBanned = false,
                    EmailConfirmed = true
                },
                new ApplicationUser
                {
                    Id = "4",
                    UserName = "sarahwilliams",
                    Email = "sarah.williams@example.com",
                    FirstName = "Sarah",
                    LastName = "Williams",
                    IsBanned = false,
                    EmailConfirmed = true
                },
                new ApplicationUser
                {
                    Id = "5",
                    UserName = "banneduser",
                    Email = "banned.user@example.com",
                    FirstName = "Banned",
                    LastName = "User",
                    IsBanned = true,
                    EmailConfirmed = true
                }
            };

                foreach (var user in users)
                    userManager.CreateAsync(user, "Password123!");

                context.SaveChanges();
            }
            if (!context.Skills.Any())
            {
                var skills = new List<Skill>
                    {
                        new Skill { Name = "Web Development" },
                        new Skill { Name = "Graphic Design" },
                        new Skill { Name = "Mobile App Development" },
                        new Skill { Name = "Content Writing" },
                        new Skill { Name = "Digital Marketing" },
                        new Skill { Name = "Data Analysis" },
                        new Skill { Name = "Video Editing" },
                        new Skill { Name = "UI/UX Design" },
                        new Skill { Name = "Content Writing" },
                        new Skill { Name = "SEO Optimization" }
                    };
                context.Skills.AddRange(skills);
                context.SaveChanges();
            }
            if (!context.Categories.Any())
            {
                var categories = new List<Category>
                    {
                        new Category { Name = "Web Development" },
                        new Category { Name = "Design & Creative" },
                        new Category { Name = "Mobile Development" },
                        new Category { Name = "Writing & Translation" },
                        new Category { Name = "Digital Marketing" },
                        new Category { Name = "Data Science & Analytics" },
                        new Category { Name = "Video & Animation" },
                        new Category { Name = "Writing & Translation" }

                    };
                context.Categories.AddRange(categories);
                context.SaveChanges();
            }
            if (!context.Profiles.Any())
            {
                var profiles = new List<Profile>
                    {
                        new Profile
                        {
                            Bio = "Experienced web developer with 5+ years of experience in building modern web applications.",
                            Image = "profile1.jpg",
                            UserId = "1"
                        },
                        new Profile
                        {
                            Bio = "Creative graphic designer specializing in branding and logo design.",
                            Image = "profile2.jpg",
                            UserId = "2"
                        },
                        new Profile
                        {
                            Bio = "Mobile app developer focused on creating user-friendly iOS and Android applications.",
                            Image = "profile3.jpg",
                            UserId = "3"
                        },
                        new Profile
                        {
                            Bio = "Professional content writer with expertise in technical and marketing content.",
                            Image = "profile4.jpg",
                            UserId = "4"
                        }
                    };
                context.Profiles.AddRange(profiles);
                context.SaveChanges();
            }
            if (!context.Projects.Any())
            {
                var projects = new List<Project>
                    {
                        new Project
                        {
                            Title = "E-commerce Website",
                            Description = "A full-featured online store with payment integration.",
                            Attachments = "project1.zip",
                            UserId = "1"
                        },
                        new Project
                        {
                            Title = "Brand Identity Design",
                            Description = "Complete branding package for a startup company.",
                            Attachments = "project2.zip",
                            UserId = "2"
                        },
                        new Project
                        {
                            Title = "Fitness Mobile App",
                            Description = "Cross-platform fitness tracking application.",
                            Attachments = "project3.zip",
                            UserId = "3"
                        },
                        new Project
                        {
                            Title = "Blog Content Series",
                            Description = "A 12-part series on digital marketing trends.",
                            Attachments = "project4.zip",
                            UserId = "4"
                        }
                    };
                context.Projects.AddRange(projects);
                context.SaveChanges();
            }
            if (!context.Jobs.Any())
            {
                var jobs = new List<Job>
                {
                    new Job
                    {
                        Title = "Website Redesign",
                        Description = "Looking for a freelancer to redesign our company website with a modern look and responsive design.",
                        Price = 1500.00m,
                        UserId = "1",
                        Deadline = DateTime.Now.AddDays(30),
                        DateCreated = DateTime.Now,
                        Skills = new List<Skill>
                        {
                            new Skill { Name = "Web Development" },
                            new Skill { Name = "Graphic Design" }
                        },
                        Categories = new List<Category>
                        {
                            new Category { Name = "Web Development" }
                        }
                    },
                    new Job
                    {
                        Title = "Mobile App Development",
                        Description = "Need an experienced mobile app developer to create a food delivery app for both iOS and Android platforms.",
                        Price = 3000.00m,
                        UserId = "2",
                        Deadline = DateTime.Now.AddDays(45),
                        DateCreated = DateTime.Now,
                        Skills = new List<Skill>
                        {
                            new Skill { Name = "Mobile App Development" },
                            new Skill { Name = "UI/UX Design" }
                        },
                        Categories = new List<Category>
                        {
                            new Category { Name = "Mobile Development" }
                        }
                    },
                    new Job
                    {
                        Title = "Content Writing for Tech Blog",
                        Description = "Seeking a content writer to produce high-quality articles on the latest technology trends.",
                        Price = 500.00m,
                        UserId = "3",
                        Deadline = DateTime.Now.AddDays(15),
                        DateCreated = DateTime.Now,
                        Skills = new List<Skill>
                        {
                            new Skill { Name = "Content Writing" },
                            new Skill { Name = "SEO Optimization" }
                        },
                        Categories = new List<Category>
                        {
                            new Category { Name = "Writing & Translation" }
                        }
                    },
                    new Job
                    {
                        Title = "Social Media Management",
                        Description = "Looking for a freelancer to manage our social media accounts and create engaging content for 3 months.",
                        Price = 1200.00m,
                        UserId = "4",
                        Deadline = DateTime.Now.AddDays(90),
                        DateCreated = DateTime.Now,
                        Skills = new List<Skill>
                        {
                            new Skill { Name = "Digital Marketing" },
                            new Skill { Name = "Content Creation" }
                        },
                        Categories = new List<Category>
                        {
                            new Category { Name = "Writing & Translation" }
                        }
                    }
                };

                context.Jobs.AddRange(jobs);
                context.SaveChanges();
            }
            if (!context.Proposals.Any())
            {
                var proposals = new List<Proposal>
                    {
                        new Proposal
                        {
                            Message = "I have extensive experience in website redesigns and can deliver a modern, responsive design within 3 weeks.",
                            Status = "Pending",
                            Price = 1400.00m,
                            FreelancerId = "2",
                            JobId = 1
                        },
                        new Proposal
                        {
                            Message = "I specialize in food delivery apps and can build this with Flutter for cross-platform compatibility.",
                            Status = "Accepted",
                            Price = 2800.00m,
                            FreelancerId = "3",
                            JobId = 2
                        },
                        new Proposal
                        {
                            Message = "I can write engaging tech blog posts with SEO optimization included.",
                            Status = "Completed",
                            Price = 450.00m,
                            FreelancerId = "4",
                            JobId = 3
                        },
                        new Proposal
                        {
                            Message = "I'll create a comprehensive social media strategy and manage your accounts for 3 months.",
                            Status = "Rejected",
                            Price = 1500.00m,
                            FreelancerId = "1",
                            JobId = 4
                        }
                    };
                context.Proposals.AddRange(proposals);
                context.SaveChanges();
            }
            if (!context.Payements.Any())
            {
                var payments = new List<Payement>
            {
                new Payement
                {
                    PaymentNumber = "PAY-001",
                    Amount = 2800.00m,
                    Status = "Paid",
                    Date = DateTime.Now.AddDays(-28)
                },
                new Payement
                {
                    PaymentNumber = "PAY-002",
                    Amount = 450.00m,
                    Status = "Paid",
                    Date = DateTime.Now.AddDays(-58)
                }
            };
                context.Payements.AddRange(payments);
                context.SaveChanges();
            }
            if (!context.Contracts.Any())
            {
                var contracts = new List<Contract>
                    {
                        new Contract
                        {
                            StartDate = DateTime.Now.AddDays(-30),
                            EndDate = DateTime.Now.AddDays(30),
                            Price = 2800.00m,
                            Status = "Active",
                            UserId = "2",
                            JobId = 1,
                            PaymentNumber = "PAY-001"
                        },
                        new Contract
                        {
                            StartDate = DateTime.Now.AddDays(-60),
                            EndDate = DateTime.Now.AddDays(-30),
                            Price = 450.00m,
                            Status = "Completed",
                            UserId = "3",
                            JobId = 3,
                            PaymentNumber = "PAY-002"
                        }
                    };
                context.Contracts.AddRange(contracts);
                context.SaveChanges();
            }
            if (!context.Reviews.Any())
            {
                var reviews = new List<Review>
                    {
                        new Review
                        {
                            Rating = 5,
                            Feedback = "Excellent work! The app was delivered on time and exceeded our expectations.",
                            Date = DateTime.Now.AddDays(-25),
                            ContractId = 1,
                            ReviewerId = "2"
                        },
                        new Review
                        {
                            Rating = 4,
                            Feedback = "Good content overall, but missed some deadlines.",
                            Date = DateTime.Now.AddDays(-35),
                            ContractId = 2,
                            ReviewerId = "3"
                        }
                    };
                context.Reviews.AddRange(reviews);
                context.SaveChanges();
            }
            if (!context.Messages.Any())
            {
                var messages = new List<Message>
                    {
                        new Message
                        {
                            Body = "Hi Jane, I'm interested in your proposal for the website redesign. Can we discuss the timeline?",
                            IsRead = true,
                            Timestamp = DateTime.Now.AddDays(-5),
                            SenderId = "1",
                            ReceiverId = "2"
                        },
                        new Message
                        {
                            Body = "Sure John, I'm available for a call tomorrow at 2 PM. Does that work for you?",
                            IsRead = true,
                            Timestamp = DateTime.Now.AddDays(-4),
                            SenderId = "2",
                            ReceiverId = "1"
                        },
                        new Message
                        {
                            Body = "I've sent the first draft of the blog posts. Please let me know your feedback.",
                            IsRead = false,
                            Timestamp = DateTime.Now.AddDays(-2),
                            SenderId = "4",
                            ReceiverId = "3"
                        }
                    };
                context.Messages.AddRange(messages);
                context.SaveChanges();
            }
            if (!context.Notifications.Any())
            {
                var notifications = new List<Notification>
                    {
                        new Notification
                        {
                            Body = "You have a new proposal for your Mobile App Development job.",
                            IsRead = true,
                            CreatedAt = DateTime.Now.AddDays(-10),
                            UserId = "2"
                        },
                        new Notification
                        {
                            Body = "Your proposal for Website Redesign was accepted!",
                            IsRead = true,
                            CreatedAt = DateTime.Now.AddDays(-8),
                            UserId = "2"
                        },
                        new Notification
                        {
                            Body = "New message from John Doe",
                            IsRead = false,
                            CreatedAt = DateTime.Now.AddDays(-1),
                            UserId = "2"
                        }
                    };
                context.Notifications.AddRange(notifications);
                context.SaveChanges();
            }
        }
    }
}