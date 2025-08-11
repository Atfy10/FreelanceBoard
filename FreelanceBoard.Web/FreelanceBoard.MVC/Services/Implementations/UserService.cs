using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;
using FreelanceBoard.MVC.Extensions;
using FreelanceBoard.MVC.Models;
using FreelanceBoard.MVC.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace FreelanceBoard.MVC.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public UserService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task LoginAsync(LoginViewModel model, HttpContext httpContext)
        {
            var client = _httpClientFactory.CreateClient("FreelanceApiClient");

            var response = await client.PostAsJsonAsync("/api/Auth/login", model);

            if (!response.IsSuccessStatusCode)
                throw new ApplicationException("Invalid email or password");

            var result = await response.Content.ReadFromJsonAsync<TokenResponse>();

            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(result?.Token);

            var claims = jwtToken.Claims.ToList();

            claims.Add(new Claim("access_token", result.Token));

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
        }
        public async Task<bool> RegisterAsync(RegisterViewModel model)
        {
            var client = _httpClientFactory.CreateClient("FreelanceApiClient");

            var response = await client.PostAsJsonAsync("/api/Auth/signup", model);

            if (response.IsSuccessStatusCode)
                return true;

            var apiError = await response.Content.ReadFromJsonAsync<ApiErrorResponse<bool?>>();
            var errorMessage = apiError?.Message ?? "An unexpected error occurred.";

            throw new ApplicationException(errorMessage);
        }
        public async Task LogoutAsync(HttpContext httpContext)
        {
            await httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }
        public async Task<UserProfileViewModel> GetProfileAsync(HttpContext httpContext)
        {
            var token = httpContext.User.GetAccessToken();

            httpContext.Items["token"] = token; //

            var userId = httpContext.User.GetUserId();

            if (string.IsNullOrWhiteSpace(userId))
                throw new ApplicationException("User not logged in");

            var client = _httpClientFactory.CreateClient("FreelanceApiClient");

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

			var response = await client.GetAsync($"/api/User/full-profile/{userId}");

            if (!response.IsSuccessStatusCode)
                throw new ApplicationException("Profile not found");

            var apiResult = await response.Content.ReadFromJsonAsync<ApiErrorResponse<UserProfileViewModel?>>();

            if (apiResult is null || !apiResult.IsSuccess)
                throw new ApplicationException(apiResult?.Message ?? "Profile not found");

            return apiResult.Data;
        }
        public async Task ChangePasswordAsync(ChangePasswordViewModel model, HttpContext httpContext)
        {
            var token = httpContext.User.GetAccessToken();

            if (string.IsNullOrEmpty(token))
                throw new ApplicationException("User not logged in.");

            //
            var userId = httpContext.User.GetUserId() ??
                throw new Exception("User ID not found in claims.");

            model.UserId = userId;

            var client = _httpClientFactory.CreateClient("FreelanceApiClient");

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.PostAsJsonAsync("/api/User/change-password", model);

			if (!response.IsSuccessStatusCode)
			{
				var errorContent = await response.Content.ReadAsStringAsync();
				throw new ApplicationException($"Failed to change password: password not correct");
			}
		}

		public async Task AddProject(AddProjectViewModel model, HttpContext httpContext)
		{
			var token = httpContext.User.GetAccessToken();
			var client = _httpClientFactory.CreateClient("FreelanceApiClient");
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

			var response = await client.PostAsJsonAsync("/api/User/add-project", model);
			if (!response.IsSuccessStatusCode)
			{
				var error = await response.Content.ReadAsStringAsync();
				throw new ApplicationException("invalid inputs");
			}


		}

		public async Task AddSkillAsync(AddSkillViewModel model, HttpContext httpContext)
		{
			var token = httpContext.User.GetAccessToken();
            var userId = httpContext.User.GetUserId();
            model.userId = userId;
			var client = _httpClientFactory.CreateClient("FreelanceApiClient");
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			var response = await client.PostAsJsonAsync("/api/User/add-skill", model);
			if (!response.IsSuccessStatusCode)
			{
				var errorContent = await response.Content.ReadAsStringAsync();
				throw new ApplicationException($"Failed to add skill: {errorContent}");
			}
		}

	}

}

