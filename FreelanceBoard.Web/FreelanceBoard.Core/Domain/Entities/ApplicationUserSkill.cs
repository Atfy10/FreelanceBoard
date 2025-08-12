using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelanceBoard.Core.Domain.Entities
{
	public class ApplicationUserSkill
	{
		public string ApplicationUsersId { get; set; }
		public ApplicationUser ApplicationUser { get; set; }

		public int SkillsId { get; set; }
		public Skill Skill { get; set; }
	}
}
