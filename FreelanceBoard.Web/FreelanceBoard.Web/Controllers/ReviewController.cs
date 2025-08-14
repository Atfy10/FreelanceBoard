using FreelanceBoard.Core.Commands.ReviewCommands;
using FreelanceBoard.Core.Queries.Interfaces;
using FreelanceBoard.Web.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FreelanceBoard.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewQuery _reviewQuery;
        private readonly IMediator _mediator;

        public ReviewController(IReviewQuery reviewQuery, IMediator mediator)
        {
            _reviewQuery = reviewQuery ?? throw new ArgumentNullException(nameof(reviewQuery));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet("get")]
        public async Task<IActionResult> GetReviewById(int id)
        {
            var reviewDto = await _reviewQuery.GetReviewByIdAsync(id);
			return this.HandleResult(reviewDto);
		}

		[HttpPost("add")]
        public async Task<IActionResult> CreateReview(CreateReviewCommand command)
        {
            var newReviewId = await _mediator.Send(command);
			return this.HandleResult(newReviewId);
		}

		[HttpGet("get-top-three")]
        public async Task<IActionResult> GetTopThreeReviews()
        {
            var reviews = await _reviewQuery.GetTopThreeReviewsAsync();
			return this.HandleResult(reviews);
		}
	}
}
