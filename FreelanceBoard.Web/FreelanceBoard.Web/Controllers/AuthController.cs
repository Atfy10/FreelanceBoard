using FreelanceBoard.Core.Domain.Entities;
using FreelanceBoard.Core.Dtos;
using FreelanceBoard.Core.Interfaces;
using FreelanceBoard.Infrastructure.JwtAuthorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
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
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly IJwtToken _iJwtToken;


        //private readonly IAuthenticationService _authenticationService;

        public AuthController(UserManager<ApplicationUser> userManager,SignInManager<ApplicationUser> signInManager, IConfiguration configuration, IJwtToken iJwtToken)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _configuration = configuration;
            _iJwtToken = iJwtToken;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null)
            {
                return Unauthorized("Invalid email or password");
            }


            //var result = await _signInManager.CheckPasswordSignInAsync(user, dto.Password, false);
            //if (!result.Succeeded) return Unauthorized("Invalid email or password");

            // Generate Token
            var token = _iJwtToken.GenerateJwtToken(user, _configuration);

            return Ok(new{token});
        }


        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            //handle in the frontend
            return Ok("Logged out");
        }

    }



}
