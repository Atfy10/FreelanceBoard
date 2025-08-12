using FreelanceBoard.MVC.Models;

namespace FreelanceBoard.MVC.Services.Interfaces
{
    public interface IProposalService
    {
        Task<List<ProposalViewModel>> GetProposalsByJobIdAsync(int jobId, HttpContext httpContext);
    }
}
