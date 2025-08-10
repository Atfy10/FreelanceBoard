using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreelanceBoard.Core.Dtos;
using FreelanceBoard.Core.Helpers;

namespace FreelanceBoard.Core.Queries.Interfaces
{
	public interface ISkillQuery
	{
		Task<Result<IEnumerable<SkillDto>>> GetAllSkillsAsync();

	}
}
