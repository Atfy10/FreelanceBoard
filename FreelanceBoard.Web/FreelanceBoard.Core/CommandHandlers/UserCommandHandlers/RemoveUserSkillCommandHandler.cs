using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FreelanceBoard.Core.Commands.UserCommands;
using FreelanceBoard.Core.Domain.Entities;
using FreelanceBoard.Core.Domain.Enums;
using FreelanceBoard.Core.Helpers;
using FreelanceBoard.Core.Interfaces;
using MediatR;

namespace FreelanceBoard.Core.CommandHandlers.UserCommandHandlers
{
	public class RemoveUserSkillCommandHandler : IRequestHandler<RemoveUserSkillCommand, Result<bool>>
	{
		private readonly ISkillRepository _skillRepository;
		private readonly IBaseRepository<ApplicationUserSkill> _userSkillRepository;
		private readonly IUserAccessor _userAccessor;
		private readonly OperationExecutor _executor;
		private readonly string DeleteOperation;
		public RemoveUserSkillCommandHandler(
			IBaseRepository<ApplicationUserSkill> userSkillRepository,
			ISkillRepository skillRepository,
			IUserAccessor userAccessor,
			OperationExecutor executor)
		{
			_userSkillRepository = userSkillRepository;
			_skillRepository = skillRepository;
			_userAccessor = userAccessor ?? throw new ArgumentNullException(nameof(userAccessor));
			_executor = executor;
			DeleteOperation = OperationType.Delete.ToString();
		}
		public async Task<Result<bool>> Handle(RemoveUserSkillCommand request, CancellationToken cancellationToken)
		=> await _executor.Execute(async () =>
		{
			if (request == null)
				throw new NullReferenceException("Remove request cannot be null.");
			var userId = _userAccessor.GetUserId();
			if (string.IsNullOrEmpty(userId))
				throw new UnauthorizedAccessException("User is not authenticated.");
			var skillId = await _skillRepository.GetIdByNameAsync(request.SkillName);
			if (!skillId.HasValue)
				throw new KeyNotFoundException($"Skill '{request.SkillName}' not found.");
			var userSkill = await _skillRepository.GetUserSkillAsync(userId, (int)skillId);
			if (userSkill == null)
				throw new KeyNotFoundException($"User skill with ID {skillId.Value} not found for user {userId}.");
			await _userSkillRepository.DeleteAsync(userSkill);
			return Result<bool>.Success(true, DeleteOperation,
				$"Skill with ID {skillId.Value} removed successfully from user {userId}.");
		}, OperationType.Delete);
	}
}
