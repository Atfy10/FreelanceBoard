
using FluentValidation;
using FreelanceBoard.Core.Domain.Entities;
using FreelanceBoard.Core.Interfaces;
using FreelanceBoard.Core.MapperProfiles;
using FreelanceBoard.Core.Validators.JobValidators;
using FreelanceBoard.Infrastructure.DBContext;
using FreelanceBoard.Infrastructure.Repositories;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FreelanceBoard.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
            b => b.MigrationsAssembly("FreelanceBoard.Infrastructure")));

            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            builder.Services.AddAutoMapper(typeof(JobAutoMapperProfile).Assembly);
            builder.Services.AddScoped<IJobRepository, JobRepository>();
            builder.Services.AddScoped<IMessageRepository, MessageRepository>();
            builder.Services.AddScoped<IContractRepository, ContractRepository>();
            builder.Services.AddScoped<IProposalRepository, ProposalRepository>();
            builder.Services.AddScoped<ISkillRepository, SkillRepository>();
            builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            builder.Services.AddMediatR(typeof(JobAutoMapperProfile).Assembly);



            builder.Services.AddValidatorsFromAssemblyContaining<CreateJobCommandValidator>();
            builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

                try
                {
                    SeedDB.Seed(dbContext, userManager);
                }
                catch (Exception ex)
                {
                    // Log the exception (you can use a logging framework here)
                    Console.WriteLine($"Seed failed: {ex.Message}." +
                        $"Inner Exception: {ex.InnerException}");
                }
            }
            app.Run();
        }
    }
}
