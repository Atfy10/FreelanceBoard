using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FreelanceBoard.Core.Domain.Entities;
using FreelanceBoard.Core.Domain.Enums;
using FreelanceBoard.Core.Helpers;
using FreelanceBoard.Core.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace FreelanceBoard.Core.Commands.UserCommands
{
	public class AddProjectCommandHandler : IRequestHandler<AddProjectCommand, Result<int>>
	{
		private readonly IBaseRepository<Project>  _projectRepository;
		private readonly IMapper _mapper;
		private readonly IUserAccessor _userAccessor;
		private readonly OperationExecutor _executor;
		private readonly string AddOperation;
		public AddProjectCommandHandler(
		IBaseRepository<Project> projectRepository,
		IMapper mapper,
		IUserAccessor userAccessor,
		OperationExecutor executor
)
		{
			_projectRepository = projectRepository;
			_mapper = mapper;
			_userAccessor = userAccessor ?? throw new ArgumentNullException(nameof(_userAccessor));
			_executor = executor;
			AddOperation = OperationType.Add.ToString();
		}

		public async Task<Result<int>> Handle(AddProjectCommand request, CancellationToken cancellationToken)
			=> await _executor.Execute(async () =>
			{
				if (request == null)
					throw new NullReferenceException("Add request cannot be null.");
				var userId = _userAccessor.GetUserId();
				if (string.IsNullOrEmpty(userId))
					throw new UnauthorizedAccessException("User is not authenticated.");
				var project = _mapper.Map<Project>(request);
				project.UserId = userId;
				await _projectRepository.AddAsync(project);
				return Result<int>.Success(project.Id, AddOperation,
					$"Project with ID {project.Id} added successfully.");
			}, OperationType.Add);
	}
}
