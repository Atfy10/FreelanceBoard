using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreelanceBoard.Core.Commands.UserCommands;
using FreelanceBoard.Core.Domain.Entities;
using FreelanceBoard.Core.Domain.Enums;
using FreelanceBoard.Core.Helpers;
using FreelanceBoard.Core.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

namespace FreelanceBoard.Core.CommandHandlers.UserCommandHandlers
{
	public class DeleteProjectCommandHandler : IRequestHandler<DeleteProjectCommand, Result<bool>>
	{
		private readonly IBaseRepository<Project> _projectRepository;
		private readonly IUserAccessor _userAccessor;
		private readonly OperationExecutor _executor;
		private readonly string DeleteOperation;

		public DeleteProjectCommandHandler(
			IBaseRepository<Project> projectRepository,
			IUserAccessor userAccessor,
			OperationExecutor executor)
		{
			_projectRepository = projectRepository;
			_userAccessor = userAccessor;
			_executor = executor;
			DeleteOperation = OperationType.Delete.ToString();
		}
		public async Task<Result<bool>> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
		=> await _executor.Execute(async () =>
		{
			var userId = _userAccessor.GetUserId();
			if (string.IsNullOrEmpty(userId))
				throw new UnauthorizedAccessException("User is not authenticated.");
			var project = await _projectRepository.GetByIdAsync(request.ProjectId) ??
					 throw new KeyNotFoundException($"Project not found.");
			if (project.UserId != userId)
				throw new UnauthorizedAccessException("You cannot delete a project that is not yours.");
			await _projectRepository.DeleteAsync(project);
			return Result<bool>.Success(true, DeleteOperation);
		}, OperationType.Delete);
	}
}
