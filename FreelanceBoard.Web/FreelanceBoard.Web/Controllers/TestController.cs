using FreelanceBoard.Core.Queries.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FreelanceBoard.Web.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TestController : ControllerBase
	{
		private readonly IMediator _mediator;
		private readonly IUserQuery _userQuery;


		public TestController(IMediator mediator, IUserQuery userQuery)
		{
			_mediator = mediator;
			_userQuery = userQuery;
		}

		[HttpGet("test")]
		public IActionResult Test()
		{
			return Ok("Test successful");
		}
		[HttpGet("error")]
		public IActionResult Error()
		{
			return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred");
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetById(string id)
		{
			var user = await _userQuery.GetUserByIdAsync(id);
			return Ok(user);
		}

		// I want make endpoint to get profile of user by id
		[HttpGet("profile/{id}")]
		public async Task<IActionResult> GetUserProfile(string id)
		{
			var user = await _userQuery.GetUserFullProfileAsync(id);
			if (user.IsSuccess)
			{
				return Ok(user);
			}
			return NotFound(user);
		}

	}
}
