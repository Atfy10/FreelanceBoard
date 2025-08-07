using FreelanceBoard.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelanceBoard.Core.Interfaces
{
    public interface IJobRepository : IBaseRepository<Job>
    {
        Task<Job?> GetFullJobWithIdAsync(int jobId);
        Task<IEnumerable<Job?>> GetAllJobsSortedDateOrBudget(bool date, bool budget, bool isAscending);

        Task<IEnumerable<Job?>> GetJobsFilteredBudget(int min, int max);
        Task<IEnumerable<Job>> GetJobsFilteredCategory(List<string> category);
        Task<IEnumerable<Job>> GetJobsFilteredSkills(List<string> skill);
    }
}
