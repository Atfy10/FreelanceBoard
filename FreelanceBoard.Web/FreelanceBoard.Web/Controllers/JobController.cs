using AutoMapper;
using FreelanceBoard.Core.Commands;
using FreelanceBoard.Core.Commands.JobCommands;
using FreelanceBoard.Core.Domain.Entities;
using FreelanceBoard.Core.Domain.Enums;
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


        [HttpGet("get-job")]
        public async Task<IActionResult> GetJobById(int id)
        {
            var jobDto = await _jobQuery.GetJobByIdAsync(id);
            return Ok(jobDto);
        }



        [HttpDelete("delete-job")]
        public async Task<IActionResult> DeleteJob(int id)
        {
            var deleted = await _mediator.Send(new DeleteJobCommand(id));
            return Ok(deleted);
        }


        [HttpPost("create-job")]
        public async Task<IActionResult> CreateJob(CreateJobCommand command)
        {
            var newJobId = await _mediator.Send(command);
            return Ok(newJobId);
        }



        [HttpPut("update-job")]
        public async Task<IActionResult> UpdateJob(UpdateJobCommand command)
        {
            var success = await _mediator.Send(command);
            return Ok(success);
        }

        [HttpGet("sorty-by")]
        public async Task<IActionResult> GetAllJobsSortedDateOrBudget(string field, int page,bool sortAscendingly)
        {
            var jobs = await _jobQuery.GetAllJobsSorted(Enum.Parse<SortBy>(field), page,sortAscendingly);
            return Ok(jobs);
        }

        [HttpGet("filter-by-skills")]
        public async Task<IActionResult> GetJobsFilteredSkills([FromQuery] List<string> skill, int page)
        {
            var jobs = await _jobQuery.GetJobsFilteredBySkills(skill, page);
            return Ok(jobs);
        }

        [HttpGet("filter-by-category")]
        public async Task<IActionResult> GetJobsFilteredCategory([FromQuery] List<string> category, int page)
        {
            var jobs = await _jobQuery.GetJobsFilteredByCategory(category,page);
            return Ok(jobs);
        }

        [HttpGet("filter-by-budget")]
        public async Task<IActionResult> GetJobsFilteredBudget(int min, int max, int page)
        {
            var jobs = await _jobQuery.GetJobsFilteredByBudget(min, max,page);
            return Ok(jobs);
        }
    }
}