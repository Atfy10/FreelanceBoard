using FreelanceBoard.MVC.Extensions;
using FreelanceBoard.MVC.Models;
using FreelanceBoard.MVC.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FreelanceBoard.MVC.Controllers
{
    public class ReviewController(IReviewService reviewService, OperationExecutor operationExecutor) : Controller
    {
        private readonly IReviewService _reviewService = reviewService;
        private readonly OperationExecutor _executor = operationExecutor;

        public async Task<IActionResult> TopThreeReviewsPartial()
        {
            ReviewViewModel[] reviews = default!;

            var success = await _executor.Execute(
                async () =>
                { reviews = await _reviewService.GetTopThreeReviewsAsync(HttpContext); },
                error => ModelState.AddModelError(string.Empty, error)
            );

            if (!success || reviews == null || reviews.Length == 0)
                return PartialView("_TopReviews", new ReviewViewModel[0]);

            return PartialView("_TopReviews", reviews.ToList());
        }
    }
}
