using FreelanceBoard.Core.Domain.Entities;
using FreelanceBoard.Core.Interfaces;
using FreelanceBoard.Infrastructure.DBContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelanceBoard.Infrastructure.Repositories
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        private readonly AppDbContext _dbContext;
        public CategoryRepository(AppDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AssignCategoryToJob(int jobId, string name)
        {
            var category = await GetCategoryByNameAsync(name);

            _dbContext.CategoryJob.Add(new CategoryJob
            { CategoriesId = category.Id, JobsId = jobId });

            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(string name)
        {
            return await _dbContext.Categories
                .AnyAsync(c => c.Name.ToLower() == name.ToLower());
        }

        public async Task<Category> GetCategoryByNameAsync(string name)
        {
            return await _dbContext.Categories
                .FirstOrDefaultAsync(c => c.Name.ToLower() == name.ToLower()) ??
                throw new KeyNotFoundException($"Category {name} is not found.");
        }
    }
}
