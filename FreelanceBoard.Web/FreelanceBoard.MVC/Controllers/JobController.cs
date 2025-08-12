using FreelanceBoard.Infrastructure.DBContext;
using FreelanceBoard.MVC.Extensions;
using FreelanceBoard.MVC.Models;
using FreelanceBoard.MVC.Services.Implementations;
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

        [AllowAnonymous]
        public async Task<IActionResult> JobListings(string sortBy = "Date")
        {
            List<JobViewModel> job = null;
            var success = await _executor.Execute(
            async () =>
                { job = await _jobService.GetAllJobsSortedAsync(HttpContext, sortBy); },
                error => ModelState.AddModelError(string.Empty, error)
            );

            if (!success || job == null)
                return View("NotFound");

            return View("JobListings", job);

        }

        [AllowAnonymous]
        public async Task<IActionResult> JobDetails(int id)
        {
            var job = await _jobService.GetJobByIdAsync(HttpContext, id);

            if (job == null || job.Id == 0)
                return View("Error", $"Job with ID {id} not found.");

            return View(job);
        }

        //This is for the Freelancers only
        public IActionResult MyJobApplication()
        {
            return View("MyJobApplication");
        }

    }
}
