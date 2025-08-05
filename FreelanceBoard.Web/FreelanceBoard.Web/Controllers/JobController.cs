using AutoMapper;
using FreelanceBoard.Core.Commands;
using FreelanceBoard.Core.Commands.JobCommands;
using FreelanceBoard.Core.Domain.Entities;
using FreelanceBoard.Core.Dtos;
using FreelanceBoard.Core.Interfaces;
using FreelanceBoard.Core.Queries;
using FreelanceBoard.Core.Queries.Interfaces;
using FreelanceBoard.Infrastructure.Repositories;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FreelanceBoard.Web.Controllers
{
    [ApiController]
    [Route("api/jobs")]
    public class JobController : ControllerBase
    {

        private readonly IMediator _mediator;
        private readonly IJobQuery _jobQuery;
        public JobController(IMediator mediator,IJobQuery jobQuery)
        {
            _mediator = mediator;
            _jobQuery = jobQuery ?? throw new ArgumentNullException(nameof(jobQuery));
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetJobById(int id)
        {
            var jobDto = await _jobQuery.GetJobByIdAsync(id);
            return Ok(jobDto);
        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJob(int id)
        {
            var deleted = await _mediator.Send(new DeleteJobCommand(id));
            return Ok(deleted);
        }


        [HttpPost]
        public async Task<IActionResult> CreateJob(CreateJobCommand command)
        {
            try
            {
                var newJobId = await _mediator.Send(command);
                if (newJobId == -1)
                    return BadRequest("One or more skills not found.");
                return Ok(newJobId);
            }
            catch (FluentValidation.ValidationException ex)
            {
                var errors = ex.Errors.Select(e => new { e.PropertyName, e.ErrorMessage });
                return BadRequest(errors); // return all validation errors nicely
            }
        }



        [HttpPut()]
        public async Task<IActionResult> UpdateJob(UpdateJobCommand command)
        {
            try
            {
                var success = await _mediator.Send(command);

                return Ok(success);
            }
            catch (FluentValidation.ValidationException ex)
            {
                var errors = ex.Errors.Select(e => new { e.PropertyName, e.ErrorMessage });
                return BadRequest(errors);
            }


        }
    }
}