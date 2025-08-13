using FreelanceBoard.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelanceBoard.Core.Interfaces
{
    public interface IReviewRepository : IBaseRepository<Review>
    {
        Task<Review?> GetFullReviewById(int id);
        Task<Review[]> GetTopThreeReviewsAsync();
    }
}
