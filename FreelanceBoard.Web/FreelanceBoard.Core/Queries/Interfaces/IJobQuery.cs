using FreelanceBoard.Core.Domain.Enums;
using FreelanceBoard.Core.Dtos.JobDtos;
using FreelanceBoard.Core.Helpers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelanceBoard.Core.Queries.Interfaces
{
    public  interface IJobQuery
    {
        Task<Result<JobDto>> GetJobByIdAsync(int id);
        Task<Result<IEnumerable<JobDto>>> GetAllJobsSorted(SortBy sortBy);
        Task<Result<IEnumerable<JobDto>>> GetJobsFilteredBySkills(List<string> skill);
        Task<Result<IEnumerable<JobDto>>> GetJobsFilteredByBudget(int min, int max);
        Task<Result<IEnumerable<JobDto>>> GetJobsFilteredByCategory(List<string> category);
        Task<Result<IEnumerable<JobDto>>> GetJobsByUserIdAsync(string userId);
    }
}
