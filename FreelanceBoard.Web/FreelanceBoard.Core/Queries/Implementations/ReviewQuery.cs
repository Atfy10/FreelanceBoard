using AutoMapper;
using FreelanceBoard.Core.Domain.Entities;
using FreelanceBoard.Core.Domain.Enums;
using FreelanceBoard.Core.Dtos;
using FreelanceBoard.Core.Dtos.JobDtos;
using FreelanceBoard.Core.Helpers;
using FreelanceBoard.Core.Interfaces;
using FreelanceBoard.Core.Queries.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelanceBoard.Core.Queries.Implementations
{
    public class ReviewQuery : IReviewQuery
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UserQuery> _logger;
        private readonly OperationExecutor _executor;
        private readonly string GetOperation;


        public ReviewQuery(IReviewRepository reviewRepository, IMapper mapper, ILogger<UserQuery> logger, OperationExecutor executor)
        {
            _reviewRepository = reviewRepository ?? throw new ArgumentNullException(nameof(reviewRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _executor = executor ?? throw new ArgumentNullException(nameof(executor));
            GetOperation = OperationType.Get.ToString();
        }

        public async Task<Result<ReviewDto>> GetReviewByIdAsync(int id)
           => await _executor.Execute(async () =>
           {
               var review = await _reviewRepository.GetFullReviewById(id) ??
                   throw new KeyNotFoundException("Review with the provided ID was not found.");

               var result = _mapper.Map<ReviewDto>(review);
               return Result<ReviewDto>.Success(result, GetOperation, "Review retrieved successfully.");
           }, OperationType.Get);

    }
}
