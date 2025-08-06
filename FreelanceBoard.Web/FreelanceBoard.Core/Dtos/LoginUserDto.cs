using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelanceBoard.Core.Dtos
{
    public class LoginUserDto : ApplicationUserDto
    {
        public string Role { get; set; }
        public string Token { get; set; }
    }
}
