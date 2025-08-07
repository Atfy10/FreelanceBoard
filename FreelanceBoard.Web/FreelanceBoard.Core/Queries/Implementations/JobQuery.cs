using AutoMapper;
using FreelanceBoard.Core.Domain.Enums;
using FreelanceBoard.Core.Dtos.JobDtos;
using FreelanceBoard.Core.Helpers;
using FreelanceBoard.Core.Interfaces;
using FreelanceBoard.Core.Queries.Implementations;
using FreelanceBoard.Core.Queries.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelanceBoard.Core.QueryHandlers.JobQueryHandlers
{
    public class JobQuery : IJobQuery
    {
        private readonly IJobRepository _jobRepository;
        private readonly IMapper _mapper;
        private readonly OperationExecutor _executor;
        private readonly string GetOperation;
        private static bool isAscending = true;

        public JobQuery(IJobRepository jobRepo, IMapper mapper, OperationExecutor executor)
        {
            _jobRepository = jobRepo;
            _mapper = mapper;
            _executor = executor ?? throw new ArgumentNullException(nameof(executor));
            GetOperation = OperationType.Get.ToString();

        }


        public async Task<Result<JobDto>> GetJobByIdAsync(int id)
        => await _executor.Execute(async () =>
        {
            var job = await _jobRepository.GetFullJobWithIdAsync(id);
            if (job == null)
                throw new ArgumentNullException(nameof(id), "Job ID was not found");

            var result = _mapper.Map<JobDto>(job);

            return Result<JobDto>.Success(result, GetOperation, $"Job with ID {id} retrieved successfully.");
        }, OperationType.Get);


        public async Task<Result<IEnumerable<JobDto>>> GetAllJobsSortedDateOrBudget(bool date, bool budget)
            => await _executor.Execute(async () =>
            {
                isAscending = !isAscending;
                if (date && budget)
                    throw new ArgumentException("Only one sorting parameter (date or budget) must be true.");

                if(!date && !budget)
                    date = true; // Default to date sorting if both are false

                var jobs = await _jobRepository.GetAllJobsSortedDateOrBudget(date, budget,isAscending);

                if (jobs == null || !jobs.Any())
                    throw new ArgumentNullException(nameof(jobs), "No jobs found.");

                var result = _mapper.Map<IEnumerable<JobDto>>(jobs);
                return Result<IEnumerable<JobDto>>.Success(result, GetOperation, "All jobs sorted accordingly retrieved successfully.");
            }, OperationType.Get);
        


        public async Task<Result<IEnumerable<JobDto>>> GetJobsFilteredBySkills(List<string> skill)
        => await _executor.Execute(async () =>
            {
                if (skill == null || skill.Count == 0)
                    throw new ArgumentNullException(nameof(skill), "Skill cannot be null or empty.");

                var jobs = await _jobRepository.GetJobsFilteredSkills(skill);

                if (jobs == null || !jobs.Any())
                    throw new ArgumentNullException(nameof(jobs), "No jobs found with the specified skill.");

                var result = _mapper.Map<IEnumerable<JobDto>>(jobs);
                return Result<IEnumerable<JobDto>>.Success(result, GetOperation, "Jobs filtered by skills retrieved successfully.");
            }, OperationType.Get);

        public async Task<Result<IEnumerable<JobDto>>> GetJobsFilteredByCategory(List<string> category)
        => await _executor.Execute(async () =>
        {
            if (category == null || category.Count == 0)
                throw new ArgumentNullException(nameof(category), "Category cannot be null or empty.");

            var jobs = await _jobRepository.GetJobsFilteredCategory(category);

            if (jobs == null || !jobs.Any())
                throw new ArgumentNullException(nameof(jobs), "No jobs found with specified category.");

            
            var result = _mapper.Map<IEnumerable<JobDto>>(jobs);

            return Result<IEnumerable<JobDto>>.Success(result, GetOperation, "Jobs filtered by category retrieved successfully");
        }, OperationType.Get);



        public async Task<Result<IEnumerable<JobDto>>> GetJobsFilteredByBudget(int min, int max)
        => await _executor.Execute(async () =>
        {
            if(min > max || min<0 )
                throw new ArgumentOutOfRangeException("Invalid budget range specified. Ensure min is less than max and both are non-negative.");

            var jobs = await _jobRepository.GetJobsFilteredBudget(min, max);

            if (jobs == null || !jobs.Any())
                throw new ArgumentNullException(nameof(jobs), "No jobs found with specified budget range.");

            var result = _mapper.Map<IEnumerable<JobDto>>(jobs);
            return Result<IEnumerable<JobDto>>.Success(result, GetOperation, "Jobs filtered by budget retrieved successfully");
        }, OperationType.Get);

    }

        
    }
