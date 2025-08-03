using FreelanceBoard.Core.Domain.Entities;
using FreelanceBoard.Core.Dtos;
using FreelanceBoard.Infrastructure.DBContext;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FreelanceBoard.Web.Controllers
{
    public class ProjectController : ControllerBase
    {

        private readonly AppDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ProjectController(AppDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [Authorize]
        [HttpPost]
        [Route("api/projects")]
        public async Task<IActionResult> CreateProject([FromForm] ProjectCreateRequest request)
        {
            if (request.File != null && request.File.Length > 0)
            {
                var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

                // Generate unique file name
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(request.File.FileName);
                var filePath = Path.Combine("wwwroot/uploads", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await request.File.CopyToAsync(stream);
                }

                var project = new Project
                {
                    Title = request.Title,
                    Description = request.Description,
                    Attachments = "/uploads/" + fileName,
                    UserId = userId
                };

                // Save project to DB (use service/repository pattern if needed)
                _context.Projects.Add(project);
                await _context.SaveChangesAsync();

                return Ok(project);
            }

            return BadRequest("No file provided.");
        }


    }
}
