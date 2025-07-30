using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelanceBoard.Core.Domain.Entities
{
    public class UserSkill
	{
		public string UserId { get; set; }
		public ApplicationUser User { get; set; }

		public int SkillId { get; set; }
		public Skill Skill { get; set; }
	}
}
