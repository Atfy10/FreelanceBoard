using FreelanceBoard.MVC.Extensions;
using FreelanceBoard.MVC.Models;
using System.Net.Http;
using System.Net.Http.Headers;

namespace FreelanceBoard.MVC.Services.Interfaces
{
    public interface IProposalService
    {
        Task<JobProposalsViewModel> GetProposalsByJobIdAsync(int jobId, HttpContext httpContext);
        Task<int> CreateProposalAsync(CreateProposalViewModel model, HttpContext httpContext);
        Task<List<JobWithProposalsViewModel>> GetProposalsByFreelancerIdAsync(string freelancerId, HttpContext httpContext);
    }
}
