using AutoMapper;
using FreelanceBoard.Core.Commands.ReviewCommands;
using FreelanceBoard.Core.Domain.Entities;
using FreelanceBoard.Core.Domain.Enums;
using FreelanceBoard.Core.Helpers;
using FreelanceBoard.Core.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelanceBoard.Core.CommandHandlers.ReviewCommandHandlers
{
    public class CreateReviewCommandHandler : IRequestHandler<CreateReviewCommand, Result<int>>
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IUserRepository _userRepository;
        private readonly IContractRepository _contractRepository;
        private readonly IMapper _mapper;
        private readonly OperationExecutor _executor;
        private readonly string CreateOperation;


        public CreateReviewCommandHandler(IReviewRepository reviewRepository,IContractRepository contractRepository, IUserRepository userRepository, IMapper mapper, OperationExecutor executor)
        {
            _contractRepository = contractRepository ?? throw new ArgumentNullException(nameof(contractRepository));
            _reviewRepository = reviewRepository ?? throw new ArgumentNullException(nameof(reviewRepository));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _executor = executor ?? throw new ArgumentNullException(nameof(executor));
            CreateOperation = OperationType.Add.ToString();
        }
        public async Task<Result<int>> Handle(CreateReviewCommand request, CancellationToken cancellationToken)
        => await _executor.Execute(async () =>
        {
            if (request == null)
                throw new NullReferenceException("Create request cannot be null.");

            var review = _mapper.Map<Review>(request);

            var user = await _userRepository.GetByIdAsync(request.ReviewerId) ??
                throw new KeyNotFoundException("Reviewer with the provided ID was not found.");

            var contract = await _contractRepository.GetFullContractWithIdAsync(request.ContractId) ??
                throw new KeyNotFoundException("Contract with the provided ID was not found.");

            review.Reviewer = user;
            review.Contract = contract;

            await _reviewRepository.AddAsync(review);
            return Result<int>.Success(review.Id, CreateOperation, "Review created successfully.");
        },OperationType.Add);
    }
}
