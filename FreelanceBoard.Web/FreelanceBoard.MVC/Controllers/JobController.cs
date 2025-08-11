using FreelanceBoard.Infrastructure.DBContext;
using FreelanceBoard.MVC.Extensions;
using FreelanceBoard.MVC.Models;
using FreelanceBoard.MVC.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace FreelanceBoard.MVC.Controllers
{
	[Authorize]

	public class JobController : Controller
    {
        private readonly IJobService _jobService;
        private readonly OperationExecutor _executor;
        public JobController(IJobService userService, OperationExecutor executor)
        {
            _jobService = userService;
            _executor = executor;
        }

        [AllowAnonymous]
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

        public async Task<IActionResult> ClientDashboard()
        {
            List<ClientDashboardViewModel> job = null;
            var success = await _executor.Execute(
                async () =>
                { job = await _jobService.GetAllJobsAsync(HttpContext); },
                error => ModelState.AddModelError(string.Empty, error)
            );

            if (!success || job == null)
                return View("NotFound");

            return View("ClientDashboard", job);
        }
    }
}
