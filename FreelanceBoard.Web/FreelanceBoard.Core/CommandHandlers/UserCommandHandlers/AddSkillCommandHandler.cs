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
	public class AddSkillCommandHandler : IRequestHandler<AddSkillCommand, Result<int>>
	{
		private readonly ISkillRepository _skillRepository;
		private readonly IMapper _mapper;
		private readonly OperationExecutor _executor;
		private readonly string AddOperation;

		public AddSkillCommandHandler(
        ISkillRepository skillRepository,
		IMapper mapper,
		OperationExecutor executor
		)
		{
			_skillRepository = skillRepository;
			_mapper = mapper;
			_executor = executor;
			AddOperation = OperationType.Add.ToString();
		}

		public Task<Result<int>> Handle(AddSkillCommand request, CancellationToken cancellationToken)
		{
			return _executor.Execute(async () =>
			{
				if (request == null)
					throw new NullReferenceException("Add request cannot be null.");

                var existingSkills = await _skillRepository.GetByNamesAsync(new List<string> { request.Name });

                if (existingSkills.Any())
                {
                    var existingSkill = existingSkills.First();
                    return Result<int>.Failure(AddOperation,$"Skill '{request.Name}' already exists");
                }

                var skill = _mapper.Map<Skill>(request);
				await _skillRepository.AddAsync(skill);
				return Result<int>.Success(skill.Id, AddOperation,
					$"Skill with ID {skill.Id} added successfully.");
			}, OperationType.Add);	




		}
	}
}
