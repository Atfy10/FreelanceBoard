using FreelanceBoard.Core.Domain.Entities;
using FreelanceBoard.Core.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FreelanceBoard.Core.Commands.UserCommands;
using MediatR;
using FreelanceBoard.Core.Helpers;
namespace FreelanceBoard.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        IMediator _mediator;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _config;

        public AuthController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager, IConfiguration config,
            IMediator mediator)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _config = config;
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

    }

}
