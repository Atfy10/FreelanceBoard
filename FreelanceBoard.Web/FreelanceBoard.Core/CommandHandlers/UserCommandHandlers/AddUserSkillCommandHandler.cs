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
	public class AddUserSkillCommandHandler : IRequestHandler<AddUserSkillCommand, Result<int>>
	{
		private readonly ISkillRepository _skillRepository;
		private readonly IBaseRepository<ApplicationUserSkill> _userSkillRepository;
		private readonly IMapper _mapper;
		private readonly IUserAccessor _userAccessor;
		private readonly OperationExecutor _executor;
		private readonly string AddOperation;
		public AddUserSkillCommandHandler(
			IBaseRepository<ApplicationUserSkill> userSkillRepository,
			ISkillRepository skillRepository,
			IMapper mapper,
			IUserAccessor userAccessor,
			OperationExecutor executor)
		{
			_userSkillRepository = userSkillRepository;
			_skillRepository = skillRepository;
			_mapper = mapper;
			_userAccessor = userAccessor ?? throw new ArgumentNullException(nameof(userAccessor));
			_executor = executor;
			AddOperation = OperationType.Add.ToString();
		}
		public async Task<Result<int>> Handle(AddUserSkillCommand request, CancellationToken cancellationToken)
			=> await _executor.Execute(async () =>
			{
				if (request == null)
					throw new NullReferenceException("Add request cannot be null.");
				var userId = _userAccessor.GetUserId();
				if (string.IsNullOrEmpty(userId))
					throw new UnauthorizedAccessException("User is not authenticated.");

				var skillId = await _skillRepository.GetIdByNameAsync(request.SkillName);
				if (!skillId.HasValue)
					throw new KeyNotFoundException($"Skill '{request.SkillName}' not found.");

				var userSkill = new ApplicationUserSkill
				{
					ApplicationUsersId = userId,
					SkillsId = skillId.Value
				};

				await _userSkillRepository.AddAsync(userSkill);
				return Result<int>.Success(userSkill.SkillsId, AddOperation,
					$"Skill with ID {userSkill.SkillsId} added successfully to user {userId}.");
			}, OperationType.Add);
	}
}
