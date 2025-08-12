using FreelanceBoard.Core.Commands.UserCommands;
using FreelanceBoard.Core.Domain.Entities;
using FreelanceBoard.Core.Dtos;
using FreelanceBoard.Core.Helpers;
using FreelanceBoard.Web.Extensions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
namespace FreelanceBoard.Web.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginCommand request)
        {
            var result = await _mediator.Send(request);

            if (!result.IsSuccess)
                return Unauthorized("Invalid credentials");

            if (result.Data?.IsBanned ?? true)
                return Unauthorized("User is banned.");

            return Ok(new { result.Data.Token });
        }

        [HttpPost("signup")]
        public async Task<IActionResult> SignUp(CreateUserCommand command)
        {
            var result = await _mediator.Send(command);

            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }
    }

}
