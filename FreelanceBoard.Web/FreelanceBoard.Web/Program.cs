
using FluentValidation;
using FreelanceBoard.Core;
using FreelanceBoard.Core.CommandHandlers;
using FreelanceBoard.Core.Commands;
using FreelanceBoard.Core.Validators;
using FreelanceBoard.Infrastructure.DBContext;
using MediatR;
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
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
	

			builder.Services.AddMediatR(typeof(CreateUserCommandHandler).Assembly);
            builder.Services.AddValidatorsFromAssemblyContaining<CreateUserCommandValidator>();
			builder.Services.AddAutoMapper(typeof(AutoMapperProfile));




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

				try
				{
					SeedDB.Seed(dbContext);
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
