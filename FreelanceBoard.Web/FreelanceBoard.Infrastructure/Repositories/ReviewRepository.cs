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
    public class ReviewRepository : BaseRepository<Review>, IReviewRepository
    {
        public ReviewRepository(AppDbContext dbContext) : base(dbContext) { }
        public async Task<Review?> GetFullReviewById(int id)
        {
            return await _dbContext.Reviews
                .Include(r => r.Contract)
                .ThenInclude(c => c.Job)
                .Include(c => c.Reviewer)
                .FirstOrDefaultAsync(r => r.Id == id);
        }
    }
}
