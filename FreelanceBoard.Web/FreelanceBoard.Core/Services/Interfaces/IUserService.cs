using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreelanceBoard.Core.Commands;

namespace FreelanceBoard.Core.Services.Interfaces
{
	public interface IUserService
	{
		Task<string> CreateUserAsync(CreateUserCommand command);
		Task<bool> DeleteUserAsync(DeleteUserCommand command);
		Task<bool> UpdateUserAsync(UpdateUserCommand command);
	}
}
