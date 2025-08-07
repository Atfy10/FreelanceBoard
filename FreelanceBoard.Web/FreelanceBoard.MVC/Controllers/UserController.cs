using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Security.Claims;
using System.Text.Json;
using FreelanceBoard.MVC.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FreelanceBoard.MVC.Controllers
{
	[Authorize]

	public class UserController : Controller
    {

        private readonly IHttpClientFactory _httpClientFactory;

        public UserController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
		[AllowAnonymous]

		public IActionResult Register()
        {
            return View("Register");
        }

        [HttpPost]
		[AllowAnonymous]

		public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var client = _httpClientFactory.CreateClient();
            var response = await client.PostAsJsonAsync("https://localhost:7029/api/Auth/signup", model);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Login", "User");
            }
			else
			{
				var errorContent = await response.Content.ReadAsStringAsync();

				var apiError = JsonSerializer.Deserialize<ApiErrorResponse<bool>>(
					errorContent,
					new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
				);

				ModelState.AddModelError(string.Empty, apiError?.Message ?? "An unexpected error occurred.");
				return View(model);
			}
		}

        [HttpGet]
		[AllowAnonymous]

		public IActionResult Login()
        {
            return View(new LoginViewModel());
        }

        // POST: UserController/Create
        [HttpPost]
        [AllowAnonymous]
		public async Task<IActionResult> Login(LoginViewModel model)
		{
			if (!ModelState.IsValid)
				return View(model);

			var client = _httpClientFactory.CreateClient();

			var response = await client.PostAsJsonAsync("https://localhost:7029/api/Auth/login", model);

			if (response.IsSuccessStatusCode)
			{
				var result = await response.Content.ReadFromJsonAsync<TokenResponse>();

				var handler = new JwtSecurityTokenHandler();
				var jwtToken = handler.ReadJwtToken(result.Token);

				var claims = jwtToken.Claims.ToList();

				// Extract important claims
				var userId = claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
				var role = claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
				var userName = claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;

				var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
				var principal = new ClaimsPrincipal(identity);

				// Sign in with cookie auth
				await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

				// Store token and userId in session 
				HttpContext.Session.SetString("token", result.Token);
				HttpContext.Session.SetString("userId", userId ?? "");
				HttpContext.Session.SetString("role", role ?? "");
				HttpContext.Session.SetString("userName", userName ?? "");

				return RedirectToAction("index", "Home");
			}

			ModelState.AddModelError(string.Empty, "Invalid email or password");
			return View(model);
		}


		[HttpPost]
		public async Task<IActionResult> Logout()
		{
			await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
			HttpContext.Session.Clear();
			return RedirectToAction("Login", "User");
		}


		//public IActionResult Profile()
  //      {
  //          return View("Profile");
  //      }

        [HttpGet]
		public async Task<IActionResult> Profile()
		{
			var userId = HttpContext.Session.GetString("userId");
			if (string.IsNullOrEmpty(userId))
			{
				return RedirectToAction("Login");
			}

			var client = _httpClientFactory.CreateClient();
			var response = await client.GetAsync($"https://localhost:7029/api/User/get-full-profile/{userId}");

			if (!response.IsSuccessStatusCode)
			{
				return View("NotFound"); 
			}

						//var content = await response.Content.ReadAsStringAsync();
			var responseData = await response.Content.ReadAsStringAsync();


			var options = new JsonSerializerOptions
			{
				PropertyNameCaseInsensitive = true
			};

			var apiResult = JsonSerializer.Deserialize<ApiErrorResponse<UserProfileViewModel>>(responseData, options);


			if (apiResult == null || !apiResult.IsSuccess)
			{
				return View("NotFound");
			}

			return View("Profile",apiResult.Data);
		}


        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View("ChangePassword");
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            ModelState.Remove(nameof(model.UserId));
            if (!ModelState.IsValid)
                return View(model);

            var token = HttpContext.Session.GetString("token");
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "User");
            }

            try
            {
                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadJwtToken(token);
                var userId = jwtToken.Claims.FirstOrDefault(c =>
                    c.Type == ClaimTypes.NameIdentifier || c.Type == "userId")?.Value;

                if (string.IsNullOrEmpty(userId))
                {
                    ModelState.AddModelError("", "User ID not found in token.");
                    return View(model);
                }

                model.UserId = userId;
            }
            catch
            {
                ModelState.AddModelError("", "Invalid token.");
                return View(model);
            }

            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.PostAsJsonAsync("https://localhost:7029/api/User/change-password", model);

            if (response.IsSuccessStatusCode)
            {
                TempData["Success"] = "Password changed successfully.";
                return RedirectToAction("Login", "User");
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                ModelState.AddModelError("", $"Failed to change password: {errorContent}");
                return View(model);
            }
        }


    }
}
