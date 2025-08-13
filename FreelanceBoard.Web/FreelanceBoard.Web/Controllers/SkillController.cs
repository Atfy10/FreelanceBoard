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
		private readonly ISkillQuery _skillQuery;
		public SkillController(IMediator mediator, ISkillQuery skillQuery)
		{
			_mediator = mediator;
			_skillQuery = skillQuery;
		}

        [HttpPost("add")]
        public async Task<IActionResult> AddSkill([FromBody] AddSkillCommand command)
        {
            if (command == null)
                return BadRequest(new { Message = "Command cannot be null." });

            var result = await _mediator.Send(command);
            return Ok(result);
        }

		[HttpGet("get-all")]
		public async Task<IActionResult> GetAllSkills()	
		{
			var result = await _skillQuery.GetAllSkillsAsync();
			return Ok(result);
		}


	}
}
