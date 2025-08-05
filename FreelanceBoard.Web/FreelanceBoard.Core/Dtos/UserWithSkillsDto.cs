using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelanceBoard.Core.Dtos
{
	public class UserWithSkillsDto
	{
		public string Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }

		public List<SkillDto> Skills { get; set; }
	}
}
