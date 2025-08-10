using FreelanceBoard.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelanceBoard.Core.Interfaces
{
    public interface ISkillRepository : IBaseRepository<Skill>
    {
        Task<List<Skill>> GetByNamesAsync(IEnumerable<string> names);
		Task<int?> GetIdByNameAsync(string name);

	}
}
