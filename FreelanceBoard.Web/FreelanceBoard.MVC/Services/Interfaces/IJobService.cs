using FreelanceBoard.MVC.Models;

namespace FreelanceBoard.MVC.Services.Interfaces
{
    public interface IJobService
    {
		Task CreateJobAsync(JobCreateViewModel model, HttpContext httpContext);
		Task<List<ClientDashboardViewModel>> GetAllJobsAsync(HttpContext httpContext, int page = 1);
        Task<List<JobViewModel>> GetAllJobsSortedAsync(HttpContext httpContext, string sortBy, int page = 1);
        Task<JobViewModel> GetJobByIdAsync(HttpContext httpContext, int id);

    }
}
