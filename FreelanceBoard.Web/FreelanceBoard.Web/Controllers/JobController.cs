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
        public JobController(IMediator mediator, IJobQuery jobQuery)
        {
            _mediator = mediator;
            _jobQuery = jobQuery ?? throw new ArgumentNullException(nameof(jobQuery));
        }


        [HttpGet("Get job")]
        public async Task<IActionResult> GetJobById(int id)
        {
            var jobDto = await _jobQuery.GetJobByIdAsync(id);
            return Ok(jobDto);
        }



        [HttpDelete("Delete job")]
        public async Task<IActionResult> DeleteJob(int id)
        {
            var deleted = await _mediator.Send(new DeleteJobCommand(id));
            return Ok(deleted);
        }


        [HttpPost("Create job")]
        public async Task<IActionResult> CreateJob(CreateJobCommand command)
        {
            var newJobId = await _mediator.Send(command);
            return Ok(newJobId);
        }



        [HttpPut("Update job")]
        public async Task<IActionResult> UpdateJob(UpdateJobCommand command)
        {
            var success = await _mediator.Send(command);
            return Ok(success);
        }

        [HttpGet("Sorted by date or budget")]
        public async Task<IActionResult> GetAllJobsSortedDateOrBudget(bool date, bool budget) // Sorting by date or budget -> true means sory by descending accordingly
        {
            var jobs = await _jobQuery.GetAllJobsSortedDateOrBudget(date, budget);
            return Ok(jobs);
        }

        [HttpGet("FilterSkills")]
        public async Task<IActionResult> GetJobsFilteredSkills([FromQuery] List<string> skill)
        {
            var jobs = await _jobQuery.GetJobsFilteredBySkills(skill);
            return Ok(jobs);
        }

        [HttpGet("FilterCategory")]
        public async Task<IActionResult> GetJobsFilteredCategory([FromQuery] List<string> category)
        {
            var jobs = await _jobQuery.GetJobsFilteredByCategory(category);
            return Ok(jobs);
        }

        [HttpGet("FilterBudget")]
        public async Task<IActionResult> GetJobsFilteredBudget(int min, int max)
        {
            var jobs = await _jobQuery.GetJobsFilteredByBudget(min, max);
            return Ok(jobs);
        }
    }
}