using FreelanceBoard.Core.Domain.Entities;
using FreelanceBoard.MVC.Extensions;
using FreelanceBoard.MVC.Models;
using FreelanceBoard.MVC.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FreelanceBoard.MVC.Controllers
{

    public class ProposalController : Controller
    {
        private readonly IProposalService _proposalService;
        private readonly OperationExecutor _executor;

        public ProposalController(IProposalService proposalService, OperationExecutor executor)
        {
            _proposalService = proposalService;
            _executor = executor;
        }

        [Authorize(Roles = "Client")]
        [HttpGet]
        public async Task<IActionResult> JobProposal(int id)
        {
            JobProposalsViewModel job = default!;
            var success = await _executor.Execute(
                async () =>
                { job = await _proposalService.GetProposalsByJobIdAsync(id, HttpContext); },
                error => ModelState.AddModelError(string.Empty, error)
            );
            TempData["token"] = User.GetAccessToken();
            if (!success || job == null)
                return View("NotFound");

            return View("JobProposal", job);
        }

        [Authorize(Roles = "Freelancer")]
        [HttpGet]
        public IActionResult CreateProposal(int id)
        {
            if (id <= 0) return NotFound();

            var model = new CreateProposalViewModel
            {
                JobId = id,
                FreelancerId = User.GetUserId() // This gets the current user's ID
            };

            return View(model);
        }

        [Authorize(Roles = "Freelancer")]
        [HttpPost]
        public async Task<IActionResult> CreateProposal(CreateProposalViewModel model)
        {
            int proposalId = 0;
            var success = await _executor.Execute(
                async () =>
                { proposalId = await _proposalService.CreateProposalAsync(model, HttpContext); },
                error => ModelState.AddModelError(string.Empty, error)
            );

            if (!success || proposalId == 0)
                return View(model);

            return RedirectToAction("FreelancerProposals");

        }

        [Authorize(Roles = "Freelancer")]
        [HttpGet]
        public async Task<IActionResult> FreelancerProposals()
        {
            List<JobWithProposalsViewModel> proposals = [];
            var success = await _executor.Execute(
                async () =>
                { proposals = await _proposalService.GetProposalsByFreelancerIdAsync(User.GetUserId(), HttpContext); },
                error => ModelState.AddModelError(string.Empty, error)
            );

            if (!success || proposals == null)
                return View("NotFound");

            return View("FreelancerProposals", proposals);
        }
    }
}
