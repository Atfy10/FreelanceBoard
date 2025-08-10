using FreelanceBoard.Core.Commands.ProposalCommands;
using FreelanceBoard.Core.Queries.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace FreelanceBoard.Web.Controllers
{
    [ApiController]
    [Route("api/proposals")]
    public class ProposalController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IProposalQuery _proposalQuery;
        public ProposalController(IMediator mediator, IProposalQuery proposalQuery)
        {
            _mediator = mediator;
            _proposalQuery = proposalQuery ?? throw new ArgumentNullException(nameof(proposalQuery));
        }

        [HttpGet("Get Proposal")]
        public async Task<IActionResult> GetProposalById(int id)
        {
            var proposalDto = await _proposalQuery.GetProposalByIdAsync(id);
            return Ok(proposalDto);
        }

        [HttpDelete("Delete Proposal")]
        public async Task<IActionResult> DeleteProposal(int id)
        {
            var deleted = await _mediator.Send(new DeleteProposalCommand(id));
            return Ok(deleted);
        }

        [HttpPost("Create Proposal")]
        public async Task<IActionResult> CreateProposal(CreateProposalCommand command)
        {
            var newProposalId = await _mediator.Send(command);
            return Ok(newProposalId);
        }

        [HttpPut("Update Proposal")]
        public async Task<IActionResult> UpdateProposal(UpdateProposalCommand command)
        {
            var success = await _mediator.Send(command);
            return Ok(success);
        }
    }
}
