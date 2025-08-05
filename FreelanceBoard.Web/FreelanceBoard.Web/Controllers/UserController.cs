using FreelanceBoard.Core.Commands.UserCommands;
using FreelanceBoard.Core.Dtos;
using FreelanceBoard.Core.Queries.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FreelanceBoard.Web.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserController : ControllerBase
	{
		private readonly IMediator _mediator;
		private readonly IUserQuery _userQuery;


		public UserController(IMediator mediator, IUserQuery userQuery)
		{
			_mediator = mediator;
			_userQuery = userQuery;
		}
		[HttpPost("signup")]
		public async Task<IActionResult> SignUp(CreateUserCommand command)
		{
			var result = await _mediator.Send(command);
			if (!result.IsSuccess)
				return BadRequest(result);

			return Ok(result);
		}

		[HttpPost("change-password")]
		public async Task<IActionResult> ChangePassword(ChangePasswordCommand command)
		{
			var result = await _mediator.Send(command);
			if (!result.IsSuccess)
				return BadRequest(result);
			return Ok(result);
		}

		[HttpDelete("delete")]
		public async Task<IActionResult> DeleteUser(DeleteUserCommand command)
		{
			var result = await _mediator.Send(command);
			if (!result.IsSuccess)
				return BadRequest(result);
			return Ok(result);
		}

		[HttpPut("update")]
		public async Task<IActionResult> UpdateUser(UpdateUserCommand command)
		{
			var result = await _mediator.Send(command);
			if (!result.IsSuccess)
				return BadRequest(result);
			return Ok(result);
		}

		[HttpGet("get-by-id/{id}")]
		public async Task<IActionResult> GetUserById(string id)
		{
			if (string.IsNullOrWhiteSpace(id))
				return BadRequest("User ID cannot be null or empty.");
			var user = await _userQuery.GetUserByIdAsync(id);
			if (user == null)
				return NotFound($"User with ID {id} not found.");
			return Ok(user);
		}

		[HttpGet("get-all")]
		public async Task<IActionResult> GetAllUsers()
		{
			var result = await _userQuery.GetAllUsersAsync();
			if (!result.IsSuccess)
				return BadRequest(result);
			return Ok(result);
		}

		[HttpGet("get-all-banned")]
		public async Task<IActionResult> GetAllBannedUsers()
		{
			var result = await _userQuery.GetAllBannedUsersAsync();
			if (!result.IsSuccess)
				return BadRequest(result);
			return Ok(result);
		}

		[HttpGet("search-by-name/{name}")]
		public async Task<IActionResult> SearchUsersByName(string name)
		{
			if (string.IsNullOrWhiteSpace(name))
				return BadRequest("Name cannot be null or empty.");
			var result = await _userQuery.SearchUsersByNameAsync(name);
			if (!result.IsSuccess)
				return BadRequest(result);
			return Ok(result);
		}

		[HttpGet("get-with-projects/{id}")]
		public async Task<IActionResult> GetUserWithProjects(string id)
		{
			if (string.IsNullOrWhiteSpace(id))
				return BadRequest("User ID cannot be null or empty.");
			var result = await _userQuery.GetUserWithProjectsAsync(id);
			if (!result.IsSuccess)
				return BadRequest(result);
			return Ok(result);
		}

		[HttpGet("get-with-skills/{id}")]
		public async Task<IActionResult> GetUserWithSkills(string id)
		{
			if (string.IsNullOrWhiteSpace(id))
				return BadRequest("User ID cannot be null or empty.");
			var result = await _userQuery.GetUserWithSkillsAsync(id);
			if (!result.IsSuccess)
				return BadRequest(result);
			return Ok(result);
		}

		[HttpGet("get-full-profile/{id}")]
		public async Task<IActionResult> GetUserFullProfile(string id)
		{
			if (string.IsNullOrWhiteSpace(id))
				return BadRequest("User ID cannot be null or empty.");
			var result = await _userQuery.GetUserFullProfileAsync(id);
			if (!result.IsSuccess)
				return BadRequest(result);
			return Ok(result);
		}



	}
}
