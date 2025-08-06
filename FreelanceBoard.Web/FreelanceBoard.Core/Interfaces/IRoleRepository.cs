using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelanceBoard.Core.Interfaces
{
    public interface IRoleRepository
    {
        /// <summary>
        /// Checks if a role exists in the system.
        /// </summary>
        /// <param name="roleName">The name of the role to check.</param>
        /// <returns>True if the role exists, otherwise false.</returns>
        Task<bool> RoleExistsAsync(string roleName);

        /// <summary>
        /// Creates a new role in the system.
        /// </summary>
        /// <param name="roleName">The name of the role to create.</param>
        /// <returns>The created role's ID.</returns>
        Task<string> CreateRoleAsync(string roleName);
    }
}
