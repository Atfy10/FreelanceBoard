using FreelanceBoard.Core.Commands.ReviewCommands;
using FreelanceBoard.Core.Queries.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FreelanceBoard.Web.Controllers
{
    [ApiController]
    [Route("api/reviews")]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewQuery _reviewQuery;
        private readonly IMediator _mediator;

        public ReviewController(IReviewQuery reviewQuery, IMediator mediator)
        {
            this._reviewQuery = reviewQuery ?? throw new ArgumentNullException(nameof(reviewQuery));
            this._mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }
        [HttpGet("Get Review")]

        public async Task<IActionResult> GetReviewById(int id)
        {
            var reviewDto = await _reviewQuery.GetReviewByIdAsync(id);

            return Ok(reviewDto);
        }

        [HttpPost("Create Review")]
        public async Task<IActionResult> CreateReview(CreateReviewCommand command)
        {
            var newReviewId = await _mediator.Send(command);
            return Ok(newReviewId);
        }
    }
}
