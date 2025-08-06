using FreelanceBoard.Core.Commands.UserCommands;
using FreelanceBoard.Core.Dtos;
using FreelanceBoard.Core.Queries.Interfaces;
using FreelanceBoard.Web.Extensions;
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
			return this.HandleResult(result, 201);
		}

		[HttpPost("change-password")]
		public async Task<IActionResult> ChangePassword(ChangePasswordCommand command)
		{
			var result = await _mediator.Send(command);
			return this.HandleResult(result);
		}

		[HttpDelete("delete")]
		public async Task<IActionResult> DeleteUser(DeleteUserCommand command)
		{
			var result = await _mediator.Send(command);
			return this.HandleResult(result, 204);
		}

		[HttpPut("update")]
		public async Task<IActionResult> UpdateUser(UpdateUserCommand command)
		{
			var result = await _mediator.Send(command);
			return this.HandleResult(result);
		}

		[HttpGet("get-by-id/{id}")]
		public async Task<IActionResult> GetUserById(string id)
		{
			if (string.IsNullOrWhiteSpace(id))
				return BadRequest(new { Message = "User ID cannot be null or empty." });

			var result = await _userQuery.GetUserByIdAsync(id);
			return this.HandleResult(result);
		}

		[HttpGet("get-all")]
		public async Task<IActionResult> GetAllUsers()
		{
			var result = await _userQuery.GetAllUsersAsync();
			return this.HandleResult(result);
		}

		[HttpGet("get-all-banned")]
		public async Task<IActionResult> GetAllBannedUsers()
		{
			var result = await _userQuery.GetAllBannedUsersAsync();
			return this.HandleResult(result);
		}

		[HttpGet("search-by-name/{name}")]
		public async Task<IActionResult> SearchUsersByName(string name)
		{
			if (string.IsNullOrWhiteSpace(name))
				return BadRequest(new { Message = "Name cannot be null or empty." });

			var result = await _userQuery.SearchUsersByNameAsync(name);
			return this.HandleResult(result);
		}

		[HttpGet("get-with-projects/{id}")]
		public async Task<IActionResult> GetUserWithProjects(string id)
		{
			if (string.IsNullOrWhiteSpace(id))
				return BadRequest(new { Message = "User ID cannot be null or empty." });

			var result = await _userQuery.GetUserWithProjectsAsync(id);
			return this.HandleResult(result);
		}

		[HttpGet("get-with-skills/{id}")]
		public async Task<IActionResult> GetUserWithSkills(string id)
		{
			if (string.IsNullOrWhiteSpace(id))
				return BadRequest(new { Message = "User ID cannot be null or empty." });

			var result = await _userQuery.GetUserWithSkillsAsync(id);
			return this.HandleResult(result);
		}

		[HttpGet("get-full-profile/{id}")]
		public async Task<IActionResult> GetUserFullProfile(string id)
		{
			if (string.IsNullOrWhiteSpace(id))
				return BadRequest(new { Message = "User ID cannot be null or empty." });

			var result = await _userQuery.GetUserFullProfileAsync(id);
			return this.HandleResult(result);
		}


	}
}
