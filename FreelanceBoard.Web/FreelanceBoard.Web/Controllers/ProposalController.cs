using FreelanceBoard.Core.Queries.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FreelanceBoard.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProposalController : ControllerBase
    {
        private readonly IProposalQuery _proposalQuery;

        public ProposalController(IProposalQuery proposalQuery)
        {
            _proposalQuery = proposalQuery;
        }

        [HttpGet("job/{jobId}/proposals")]
        public async Task<IActionResult> GetProposalsByJobId(int jobId)
        {
            if (jobId <= 0)
                return BadRequest("Invalid job ID.");

            var proposals = await _proposalQuery.GetProposalsByJobIdAsync(jobId);
            return Ok(proposals);
        }
    }
}
