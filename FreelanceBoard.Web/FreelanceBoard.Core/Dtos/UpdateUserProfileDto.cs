using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelanceBoard.Core.Dtos
{
	public class UpdateUserProfileDto
	{
		public string UserName { get; set; }
		public string PhoneNumber { get; set; }

		public string Bio{ get; set; }
	}
}
