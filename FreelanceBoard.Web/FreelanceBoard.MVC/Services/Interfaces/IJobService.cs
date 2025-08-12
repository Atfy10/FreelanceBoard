using FreelanceBoard.MVC.Models;

namespace FreelanceBoard.MVC.Services.Interfaces
{
    public interface IJobService
    {
        Task<List<ClientDashboardViewModel>> GetAllJobsAsync(HttpContext httpContext);
        Task<List<JobViewModel>> GetAllJobsSortedAsync(HttpContext httpContext, string sortBy);
        Task<JobViewModel> GetJobByIdAsync(HttpContext httpContext, int id);

    }
}
