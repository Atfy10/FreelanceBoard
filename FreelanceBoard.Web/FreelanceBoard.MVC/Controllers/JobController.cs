using Microsoft.AspNetCore.Mvc;

namespace FreelanceBoard.MVC.Controllers
{
    public class JobController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult JobListings()
        {
            return View("JobListings");
        }

        public IActionResult JobDetails()
        {
            return View("JobDetails");
        }
    }
}
