using System.IdentityModel.Tokens.Jwt;
using System.IO.Pipes;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Security.Claims;
using System.Text.Json;
using FreelanceBoard.MVC.Extensions;
using FreelanceBoard.MVC.Models;
using FreelanceBoard.MVC.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FreelanceBoard.MVC.Controllers
{
    [Authorize]

    public class UserController : Controller
    {

        private readonly IUserService _userService;
        private readonly OperationExecutor _executor;
        //private readonly IHttpClientFactory _httpClientFactory;
        public UserController(IUserService userService, OperationExecutor executor,
            IHttpClientFactory httpClientFactory)
        {
            _userService = userService;
            _executor = executor;
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

            var success = await _executor.Execute(
                () => _userService.RegisterAsync(model),
                error => ModelState.AddModelError(string.Empty, error)
            );

            if (success)
                return RedirectToAction("Login", "User");

            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View(new LoginViewModel());
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var success = await _executor.Execute(
                () => _userService.LoginAsync(model, HttpContext),
                error => ModelState.AddModelError(string.Empty, error)
            );

            if (success)
                return RedirectToAction("Index", "Home");

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            var success = await _executor.Execute(
                () => _userService.LogoutAsync(HttpContext),
                error => ModelState.AddModelError(string.Empty, error)
            );

            return RedirectToAction("Login", "User");
        }

        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            UserProfileViewModel profile = null;

            var success = await _executor.Execute(
                async () =>
                { profile = await _userService.GetProfileAsync(HttpContext); },
                error => ModelState.AddModelError(string.Empty, error)
            );

            if (!success || profile == null)
                return View("NotFound");

            TempData["token"] = HttpContext.Items["token"]; //

            return View("Profile", profile);
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

            var success = await _executor.Execute(
                async () =>
                {
                    await _userService.ChangePasswordAsync(model, HttpContext);
                    TempData["Success"] = "Password changed successfully.";
                },
                error => ModelState.AddModelError(string.Empty, error)
            );

            if (success)
                return RedirectToAction("Login", "User");

            return View(model);
        }

    
		[HttpPost]
		public async Task<IActionResult> AddProject([FromBody] AddProjectViewModel model)
		{
			if (!ModelState.IsValid) return View(model);
			var success = await _executor.Execute(
				async () =>
				{
					await _userService.AddProject(model, HttpContext);
				},
				error => ModelState.AddModelError(string.Empty, error)
				);
			return RedirectToAction("Project", "User");
		}

		[HttpPost]
		public async Task<IActionResult> AddSkill([FromBody] AddSkillViewModel request)
		{
			if (!ModelState.IsValid) return View(request);
			var success = await _executor.Execute(
				async () =>
				{
					await _userService.AddSkillAsync(request, HttpContext);
				},
				error => ModelState.AddModelError(string.Empty, error)
			);
			if (!success)
				return View(request);
			return RedirectToAction("Profile", "User");
		}
	}
}
