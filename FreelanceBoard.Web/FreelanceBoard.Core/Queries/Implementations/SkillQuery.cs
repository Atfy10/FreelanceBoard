using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FreelanceBoard.Core.Domain.Entities;
using FreelanceBoard.Core.Domain.Enums;
using FreelanceBoard.Core.Dtos;
using FreelanceBoard.Core.Dtos.JobDtos;
using FreelanceBoard.Core.Helpers;
using FreelanceBoard.Core.Interfaces;
using FreelanceBoard.Core.Queries.Interfaces;
using Microsoft.Extensions.Logging;

namespace FreelanceBoard.Core.Queries.Implementations
{
	public class SkillQuery : ISkillQuery
	{
		private readonly IBaseRepository<Skill> _skillRepository;
		private readonly IMapper _mapper;
		private readonly OperationExecutor _executor;

		private readonly string GetALLOperation;


		public SkillQuery(IBaseRepository<Skill> skillRepository, IMapper mapper, OperationExecutor executor)
		{
			_skillRepository = skillRepository;
			_mapper = mapper;
			_executor = executor;
			GetALLOperation = OperationType.GetAll.ToString();
		}

		public async Task<Result<IEnumerable<SkillDto>>> GetAllSkillsAsync()
		=> await _executor.Execute(async () =>
		{
			var skills = await _skillRepository.GetAllAsync(); 
			var skillsDto = _mapper.Map<IEnumerable<SkillDto>>(skills);
			return Result<IEnumerable<SkillDto>>.Success(skillsDto, GetALLOperation, "all skills retreived succesfully");
		}, OperationType.GetAll);
	}
}
