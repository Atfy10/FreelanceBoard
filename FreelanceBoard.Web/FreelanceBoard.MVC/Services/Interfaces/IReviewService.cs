using FreelanceBoard.MVC.Models;

namespace FreelanceBoard.MVC.Services.Interfaces
{
    public interface IReviewService
    {
        public Task<ReviewViewModel[]> GetTopThreeReviewsAsync(HttpContext httpContext);

    }
}
