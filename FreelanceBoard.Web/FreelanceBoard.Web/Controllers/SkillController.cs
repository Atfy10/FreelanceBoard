using FreelanceBoard.Core.Commands.UserCommands;
using FreelanceBoard.Core.Helpers;
using FreelanceBoard.Core.Queries.Interfaces;
using FreelanceBoard.Web.Extensions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FreelanceBoard.Web.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class SkillController : ControllerBase
	{
		private readonly IMediator _mediator;
		private readonly IUserQuery _userQuery;
		public SkillController(IMediator mediator, IUserQuery userQuery)
		{
			_mediator = mediator;
			_userQuery = userQuery;
		}

		[HttpPost]
		public async Task<IActionResult> AddSkill([FromBody] AddSkillCommand command)
		{
			if (command == null)
				return BadRequest(new { Message = "Command cannot be null." });
			var result = await _mediator.Send(command);
			return this.HandleResult(result);
		}
	}
}
