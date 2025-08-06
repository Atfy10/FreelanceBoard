using FreelanceBoard.MVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace FreelanceBoard.MVC.Controllers
{
    public class UserController : Controller
    {
        //public IActionResult Index()
        //{
        //    return View();
        //}
        private readonly IHttpClientFactory _httpClientFactory;

        public UserController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public IActionResult Register()
        {
            return View("Register");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View(new LoginViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            //if (!ModelState.IsValid)
            //    return View(model);

            var client = _httpClientFactory.CreateClient();

            var response = await client.PostAsJsonAsync("https://localhost:7029/api/Auth/login", model);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<TokenResponse>();

                HttpContext.Session.SetString("token", result.Token); // Or store in cookie

                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Invalid login attempt");
            return View(model);
        }

        public IActionResult Profile()
        {
            return View("Profile");
        }
    }
}
