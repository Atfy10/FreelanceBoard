using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelanceBoard.Core.Interfaces
{
    public interface IJwtToken
    {

        public string GenerateJwtToken(IdentityUser user, IConfiguration configuration);
    }
}
