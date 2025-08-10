using FreelanceBoard.MVC.Models;

namespace FreelanceBoard.MVC.Services.Interfaces
{
    public interface IJobService
    {
        Task<List<ClientDashboardViewModel>> GetAllJobsAsync(HttpContext httpContext);

    }
}
