using FreelanceBoard.Core.Domain.Entities;
using FreelanceBoard.Core.Dtos;
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
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _config;

        public AuthController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IConfiguration config)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _config = config;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
                return Unauthorized("Invalid credentials");

            if (user.IsBanned)
                return Unauthorized("User is banned.");

            //var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
            //if (!result.Succeeded)
            //    return Unauthorized("Invalid credentials");

            var roles = await _userManager.GetRolesAsync(user);

            var token = GenerateJwtToken(user, roles);
            return Ok(new { Token = token });
        }

        private string GenerateJwtToken(ApplicationUser user, IList<string> roles)
        {
            var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Name, user.UserName)
        };

            foreach (var role in roles)
                claims.Add(new Claim(ClaimTypes.Role, role));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

}
