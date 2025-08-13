using FreelanceBoard.MVC.Models;

namespace FreelanceBoard.MVC.Services.Interfaces
{
    public interface IProposalService
    {
        Task<JobProposalsViewModel> GetProposalsByJobIdAsync(int jobId, HttpContext httpContext);

        Task<int> CreateProposalAsync(CreateProposalViewModel model, HttpContext httpContext);
    }
}
