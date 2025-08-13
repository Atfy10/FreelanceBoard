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
using System.ComponentModel.DataAnnotations;

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
			return Ok(result);
		}

		[HttpDelete("delete")]
		public async Task<IActionResult> DeleteUser(DeleteUserCommand command)
		{
			var result = await _mediator.Send(command);
			return Ok(result);
		}

		[HttpPut("update")]
		public async Task<IActionResult> UpdateUser(UpdateUserCommand command)
		{
			var result = await _mediator.Send(command);
			return Ok(result);
		}

		[HttpGet("get/{id}")]
		public async Task<IActionResult> GetUserById(string id)
		{
			var result = await _userQuery.GetUserByIdAsync(id);
			return Ok(result);
		}

		[HttpGet("get-all")]
		public async Task<IActionResult> GetAllUsers()
		{
			var result = await _userQuery.GetAllUsersAsync();
			return Ok(result);
		}

		[HttpGet("get-all-banned")]
		public async Task<IActionResult> GetAllBannedUsers()
		{
			var result = await _userQuery.GetAllBannedUsersAsync();
			return Ok(result);
		}

		[HttpGet("get-search-by-name/{name}")]
		public async Task<IActionResult> SearchUsersByName(string name)
		{
			var result = await _userQuery.SearchUsersByNameAsync(name);
			return Ok(result);
		}

		[HttpGet("get-with-projects/{id}")]
		public async Task<IActionResult> GetUserWithProjects(string id)
		{
			var result = await _userQuery.GetUserWithProjectsAsync(id);
			return Ok(result);
		}

		[HttpGet("get-with-skills/{id}")]
		public async Task<IActionResult> GetUserWithSkills(string id)
		{
			var result = await _userQuery.GetUserWithSkillsAsync(id);
			return Ok(result);
		}

		[HttpGet("get-full-profile/{id}")]
		public async Task<IActionResult> GetUserFullProfile(string id)
		{
			var result = await _userQuery.GetUserFullProfileAsync(id);
			return Ok(result);
		}

        [HttpPost("change-profile-picture")]
        public async Task<IActionResult> ChangeProfilePicture([FromForm] ChangeProfilePictureCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

		[HttpPost("add-project")]
		public async Task<IActionResult> AddUserProject([FromBody] AddProjectCommand command)
		{
			var result = await _mediator.Send(command);
			return Ok(result);
		}

		[HttpPost("add-skill")]
		public async Task<IActionResult> AddSkill([FromBody] AddUserSkillCommand command)
		{
			if (command == null)
				return BadRequest(new { Message = "Command cannot be null." });
			var result = await _mediator.Send(command);
			return Ok(result);
		}

		//make delete user skill endpoint
		[HttpPost("remove-skill")]
		public async Task<IActionResult> RemoveSkill([FromBody] RemoveUserSkillCommand command)
		{
			if (command == null)
				return BadRequest(new { Message = "Command cannot be null." });
			var result = await _mediator.Send(command);
			return Ok(result);
		}

		[HttpPut("update-profile")]
		public async Task<IActionResult> UpdateProfile(UpdateUserProfileCommand command)
		{
			if (command == null)
				return BadRequest(new { Message = "Command cannot be null." });
			var result = await _mediator.Send(command);
			return Ok(result);
		}

		[HttpDelete("delete-project/{projectId}")]
		public async Task<IActionResult> DeleteProject(int projectId)
		{
			var command = new DeleteProjectCommand { ProjectId = projectId };
			var result = await _mediator.Send(command);
			return Ok(result);
		}
	}
}
