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
        public SkillController(IMediator mediator, IUserQuery userQuery)
        {
            _mediator = mediator;
        }
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
    }
		[HttpPost]
		public async Task<IActionResult> AddSkill([FromBody] AddSkillCommand command)
		{
			if (command == null)
				return BadRequest(new { Message = "Command cannot be null." });
			var result = await _mediator.Send(command);
			return this.HandleResult(result);
		}

		//get all skills
		[HttpGet]
		public async Task<IActionResult> GetAllSkills()
		{
			var result = await _skillQuery.GetAllSkillsAsync();
			return this.HandleResult(result);
		}


	}
}
