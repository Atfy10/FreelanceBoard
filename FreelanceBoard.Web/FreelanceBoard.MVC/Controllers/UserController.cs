using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Security.Claims;
using System.Text.Json;
using FreelanceBoard.MVC.Extensions;
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

			var client = _httpClientFactory.CreateClient("FreelanceApiClient");
			var response = await client.PostAsJsonAsync("/api/Auth/signup", model);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Login", "User");
            }
            else
            {
				var apiError = await response.Content.ReadFromJsonAsync<ApiErrorResponse<bool>>();

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

			var client = _httpClientFactory.CreateClient("FreelanceApiClient");

			var response = await client.PostAsJsonAsync("/api/Auth/login", model);

			if (response.IsSuccessStatusCode)
			{
				var result = await response.Content.ReadFromJsonAsync<TokenResponse>();

				var handler = new JwtSecurityTokenHandler();
				var jwtToken = handler.ReadJwtToken(result.Token);

				var claims = jwtToken.Claims.ToList();

				// Add the JWT token itself as a claim
				claims.Add(new Claim("access_token", result.Token));



				// Extract important claims
				//var userId = claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

				var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
				var principal = new ClaimsPrincipal(identity);

				// Sign in with cookie auth
				await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);


				return RedirectToAction("index", "Home");
			}

			ModelState.AddModelError(string.Empty, "Invalid email or password");
			return View(model);
		}


		[HttpPost]
		public async Task<IActionResult> Logout()
		{
			await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
			return RedirectToAction("Login", "User");
		}



        [HttpGet]
		public async Task<IActionResult> Profile()
		{
			//var token = HttpContext.Session.GetString("token");
            var token= User.GetAccessToken();
			TempData["token"] = token; 

            var userId = User.GetUserId();
			if (string.IsNullOrEmpty(userId))
			{
				return RedirectToAction("Login");
			}

			var client = _httpClientFactory.CreateClient("FreelanceApiClient");
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			var response = await client.GetAsync($"/api/User/get-full-profile/{userId}");

			if (!response.IsSuccessStatusCode)
			{
				return View("NotFound"); 
			}

			var apiResult = await response.Content.ReadFromJsonAsync<ApiErrorResponse<UserProfileViewModel>>();

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

            var token = User.GetAccessToken();

			if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "User");
            }

			var userId = User.GetUserId();
			model.UserId = userId;

            var client = _httpClientFactory.CreateClient("FreelanceApiClient");
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.PostAsJsonAsync("/api/User/change-password", model);

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
