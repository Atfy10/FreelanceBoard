using FreelanceBoard.Core.Commands;
using FreelanceBoard.Core.Domain.Entities;
using FreelanceBoard.Core.Dtos;
using FreelanceBoard.Infrastructure.DBContext;
using MediatR;
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
        private readonly IMediator _mediator;
        public ProjectController(AppDbContext context, UserManager<ApplicationUser> userManager, IMediator mediator)
        {
            _context = context;
            _userManager = userManager;
            _mediator = mediator;
        }

        [Authorize]
        [HttpPost]
        [Route("api/projects")]
        public async Task<IActionResult> CreateProject([FromForm] CreateFileCommand request)
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            request.UserId = userId;
            var result =await _mediator.Send(request);
            if(result == true)
            {

            return Ok(result);
            }

            return BadRequest("No file provided.");
        }


    }
}
