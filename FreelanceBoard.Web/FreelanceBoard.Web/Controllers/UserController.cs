using FreelanceBoard.Core.Commands;
using FreelanceBoard.Core.Commands.UserCommands;
using FreelanceBoard.Core.Domain.Entities;
using FreelanceBoard.Core.Dtos;
using FreelanceBoard.Core.Queries.Interfaces;
using FreelanceBoard.Infrastructure.DBContext;
using FreelanceBoard.Web.Extensions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

		[HttpPost("change-password")]
		public async Task<IActionResult> ChangePassword(ChangePasswordCommand command)
		{
			var result = await _mediator.Send(command);
			return this.HandleResult(result);
		}

		[HttpDelete]
		public async Task<IActionResult> DeleteUser(DeleteUserCommand command)
		{
			var result = await _mediator.Send(command);
			return this.HandleResult(result, 204);
		}

		[HttpPut]
		public async Task<IActionResult> UpdateUser(UpdateUserCommand command)
		{
			var result = await _mediator.Send(command);
			return this.HandleResult(result);
		}

		[HttpGet("/{id}")]
		public async Task<IActionResult> GetUserById(string id)
		{
			if (string.IsNullOrWhiteSpace(id))
				return BadRequest(new { Message = "User ID cannot be null or empty." });

			var result = await _userQuery.GetUserByIdAsync(id);
			return this.HandleResult(result);
		}

		[HttpGet("all")]
		public async Task<IActionResult> GetAllUsers()
		{
			var result = await _userQuery.GetAllUsersAsync();
			return this.HandleResult(result);
		}

		[HttpGet("all-banned")]
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

		[HttpGet("with-projects/{id}")]
		public async Task<IActionResult> GetUserWithProjects(string id)
		{
			if (string.IsNullOrWhiteSpace(id))
				return BadRequest(new { Message = "User ID cannot be null or empty." });

			var result = await _userQuery.GetUserWithProjectsAsync(id);
			return this.HandleResult(result);
		}

		[HttpGet("with-skills/{id}")]
		public async Task<IActionResult> GetUserWithSkills(string id)
		{
			if (string.IsNullOrWhiteSpace(id))
				return BadRequest(new { Message = "User ID cannot be null or empty." });

			var result = await _userQuery.GetUserWithSkillsAsync(id);
			return this.HandleResult(result);
		}

		[HttpGet("full-profile/{id}")]
		public async Task<IActionResult> GetUserFullProfile(string id)
		{
			if (string.IsNullOrWhiteSpace(id))
				return BadRequest(new { Message = "User ID cannot be null or empty." });

			var result = await _userQuery.GetUserFullProfileAsync(id);
			return this.HandleResult(result);
		}

        [HttpPost("ChangeProfilePicture")]
        public async Task<IActionResult> ChangeProfilePicture([FromForm] ChangeProfilePictureCommand command)
        {
            var result = await _mediator.Send(command);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

		[HttpPost("add-project")]

		public async Task<IActionResult> AddProject([FromBody] AddProjectCommand command)
		{
			if (command == null)
				return BadRequest(new { Message = "Command cannot be null." });
			var result = await _mediator.Send(command);
			return this.HandleResult(result);
		}

		[HttpPost("add-skill")]

		public async Task<IActionResult> AddSkill([FromBody] AddUserSkillCommand command)
		{
			if (command == null)
				return BadRequest(new { Message = "Command cannot be null." });
			var result = await _mediator.Send(command);
			return this.HandleResult(result);
		}

		//make delete user skill endpoint
		[HttpDelete("remove-skill")]
		public async Task<IActionResult> RemoveSkill([FromBody] RemoveUserSkillCommand command)
		{
			if (command == null)
				return BadRequest(new { Message = "Command cannot be null." });
			var result = await _mediator.Send(command);
			return this.HandleResult(result);
		}




	}
}
