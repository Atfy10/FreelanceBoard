using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelanceBoard.Core.Domain.Entities
{
    public class Skill
    {
        [Key]
        public int Id { get; set; }
        public required string Name { get; set; }
		//after review i think each skill can have many users
		//public virtual ICollection<ApplicationUser> ApplicationUsers { get; set; }
		public virtual ICollection<UserSkill> UserSkills { get; set; } = new List<UserSkill>();
		public virtual ICollection<JobSkill> JobSkills { get; set; } = new List<JobSkill>();

		//public virtual ICollection<Job> Jobs { get; set; }
    }
}
