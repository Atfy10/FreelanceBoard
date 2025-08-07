using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FreelanceBoard.MVC.Controllers
{
	[Authorize]

	public class JobController : Controller
    {
        public IActionResult Index()
        {
            return View("JobListings");
        }

        //public IActionResult JobListings()
        //{
        //    return View();
        //}

        public IActionResult JobDetails(int id)
        {
            return View("JobDetails");
        }
    }
}
