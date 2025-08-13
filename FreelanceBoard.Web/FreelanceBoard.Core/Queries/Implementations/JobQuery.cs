using AutoMapper;
using FreelanceBoard.Core.Domain.Enums;
using FreelanceBoard.Core.Dtos.JobDtos;
using FreelanceBoard.Core.Exceptions;
using FreelanceBoard.Core.Helpers;
using FreelanceBoard.Core.Interfaces;
using FreelanceBoard.Core.Queries.Implementations;
using FreelanceBoard.Core.Queries.Interfaces;
using MediatR;
using Microsoft.Extensions.Configuration;
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
        private const int PAGESIZE = 5;

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
                var job = await _jobRepository.GetFullJobWithIdAsync(id) ??
                    throw new KeyNotFoundException($"Job with ID {id} not found.");

                var result = _mapper.Map<JobDto>(job);

                return Result<JobDto>.Success(result, GetOperation, $"Job with ID {id} retrieved successfully.");
            }, OperationType.Get);

        public async Task<Result<IEnumerable<JobDto>>> GetAllJobsSorted(string field, int page, bool sortAscendingly)
            => await _executor.Execute(async () =>
            {
                if (!Enum.TryParse(field, true, out SortBy sortBy))
                    throw new InvalidSortChoiceException($"Invalid sort field specified: {field}. Valid options are 'Date' or 'Budget'.");

                var jobs = sortBy switch
                {
                    SortBy.Date => await _jobRepository.GetAllJobsSortByDate(sortAscendingly),
                    SortBy.Budget => await _jobRepository.GetAllJobsSortByBudget(sortAscendingly),
                    _ => throw new ArgumentException("Invalid sorting parameter specified."),
                };

                if (jobs == null || !jobs.Any())
                    throw new ArgumentNullException(nameof(jobs), "No jobs found.");

                var result = _mapper.Map<IEnumerable<JobDto>>(jobs);
                var resultPaginated = Page(result, page, PAGESIZE);
                return Result<IEnumerable<JobDto>>.Success(resultPaginated, GetOperation, "All jobs sorted accordingly retrieved successfully.");
            }, OperationType.Get);

        public async Task<Result<IEnumerable<JobDto>>> GetJobsFilteredBySkills(List<string> skills, int page)
            => await _executor.Execute(async () =>
            {
                if (skills == null || skills.Count == 0)
                    throw new ArgumentNullException(nameof(skills), "Skill cannot be null or empty.");

                var jobs = await _jobRepository.GetJobsFilteredSkills(skills);

                if (jobs == null || !jobs.Any())
                return Result<IEnumerable<JobDto>>.Success(
                       Enumerable.Empty<JobDto>(),GetOperation,
                       $"No jobs matched ALL specified skills: {string.Join(", ", skills)}");

                var result = _mapper.Map<IEnumerable<JobDto>>(jobs);
                var resultPaginated = Page(result, page, PAGESIZE);
                return Result<IEnumerable<JobDto>>.Success(resultPaginated, GetOperation, "Jobs filtered by skills retrieved successfully.");
            }, OperationType.Get);

        public async Task<Result<IEnumerable<JobDto>>> GetJobsFilteredByCategory(List<string> categories, int page)
            => await _executor.Execute(async () =>
            {
                if (categories == null || categories.Count == 0)
                    throw new ArgumentNullException(nameof(categories), "Category cannot be null or empty.");

                var jobs = await _jobRepository.GetJobsFilteredCategory(categories);

                if (jobs == null || !jobs.Any())
                    return Result<IEnumerable<JobDto>>.Success(
                           Enumerable.Empty<JobDto>(),
                           GetOperation,
                           $"No jobs matched ALL specified categories: {string.Join(", ", categories)}");


                var result = _mapper.Map<IEnumerable<JobDto>>(jobs);

                var resultPaginated = Page(result, page, PAGESIZE);

                return Result<IEnumerable<JobDto>>.Success(resultPaginated, GetOperation, "Jobs filtered by category retrieved successfully");
            }, OperationType.Get);

        public async Task<Result<IEnumerable<JobDto>>> GetJobsFilteredByBudget(int min, int max, int page)
            => await _executor.Execute(async () =>
            {
                if (min > max || min < 0)
                    throw new ArgumentOutOfRangeException("Invalid budget range specified. Ensure min is less than max and both are non-negative.");

                var jobs = await _jobRepository.GetJobsFilteredBudget(min, max);

                if (jobs == null || !jobs.Any())
                    return Result<IEnumerable<JobDto>>.Success([], GetOperation, "No jobs matched the given range.");

                var result = _mapper.Map<IEnumerable<JobDto>>(jobs);
                var resultPaginated = Page(result, page, PAGESIZE);
                return Result<IEnumerable<JobDto>>.Success(resultPaginated, GetOperation, "Jobs filtered by budget retrieved successfully");
            }, OperationType.Get);

        public async Task<Result<IEnumerable<JobDto>>> GetJobsByUserIdAsync(string userId)
            => await _executor.Execute(async () =>
            {
                if (string.IsNullOrWhiteSpace(userId))
                    throw new ArgumentNullException(nameof(userId), "User ID cannot be null or empty.");

                var jobs = await _jobRepository.GetJobsByUserIdAsync(userId);

                if (jobs == null || !jobs.Any())
                    return Result<IEnumerable<JobDto>>.Failure(GetOperation, "No jobs found for this user.");

                var result = _mapper.Map<IEnumerable<JobDto>>(jobs);
                return Result<IEnumerable<JobDto>>.Success(result, GetOperation, "Jobs for the user retrieved successfully.");
            }, OperationType.Get);


        private static IEnumerable<T> Page<T>(IEnumerable<T> source, int page, int pageSize)
        {
            if (source is null) return Enumerable.Empty<T>();
            if (page < 1)
                throw new ArgumentOutOfRangeException(nameof(page), "page id cannot be less than 1");

            page = Math.Max(1, page);
            pageSize = Math.Clamp(pageSize, 1, 50);
            var skip = (page - 1) * pageSize;
            return source.Skip(skip).Take(pageSize);
        }
    }
}
