using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreelanceBoard.Core.Domain.Entities;

namespace FreelanceBoard.Core.Interfaces
{
	public interface IProfileRepository : IBaseRepository<Profile>
	{
		Task<Profile> GetByUserIdAsync(string userId);

	}
}
