using Microsoft.AspNetCore.Mvc;

namespace FreelanceBoard.MVC.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        
    }
}
