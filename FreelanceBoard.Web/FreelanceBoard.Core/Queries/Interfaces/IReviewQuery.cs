using FreelanceBoard.Core.Dtos;
using FreelanceBoard.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelanceBoard.Core.Queries.Interfaces
{
    public interface IReviewQuery
    {
        Task<Result<ReviewDto>> GetReviewByIdAsync(int id);
    }
}
