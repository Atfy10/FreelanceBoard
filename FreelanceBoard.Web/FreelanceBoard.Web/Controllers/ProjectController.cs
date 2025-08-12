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
    [Authorize]
    [Route("api/[controller]")]
    public class ProjectController : ControllerBase
    {

        private readonly IMediator _mediator;
        public ProjectController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("add")]
        public async Task<IActionResult> CreateProject([FromForm] CreateFileCommand request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }


    }
}
