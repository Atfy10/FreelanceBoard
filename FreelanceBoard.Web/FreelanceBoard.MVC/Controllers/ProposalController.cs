using FreelanceBoard.Core.Domain.Entities;
using FreelanceBoard.MVC.Extensions;
using FreelanceBoard.MVC.Models;
using FreelanceBoard.MVC.Services.Interfaces;
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
        public async Task<IActionResult> JobProposal(int id)
        {
            List<ProposalViewModel> job = null;
            var success = await _executor.Execute(
                async () =>
                { job = await _proposalService.GetProposalsByJobIdAsync(id, HttpContext); },
                error => ModelState.AddModelError(string.Empty, error)
            );

            if (!success || job == null)
                return View("NotFound");

            return View("JobProposal",job);
        }
    }
}
