using FreelanceBoard.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelanceBoard.Core.Interfaces
{
    public interface ICategoryRepository : IBaseRepository<Category>
    {
        Task<bool> ExistsAsync(string name);
        Task AssignCategoryToJob(int jobId, string name);
        Task<Category> GetCategoryByNameAsync(string name);
    }
}
