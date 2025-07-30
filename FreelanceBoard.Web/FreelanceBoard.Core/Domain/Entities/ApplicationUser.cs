using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelanceBoard.Core.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public bool IsBanned { get; set; }

        //Navigation Properties
        public virtual ICollection<Job> Jobs { get; set; }
        public virtual Profile Profile { get; set; }

		// after REVIEW i think each user can have more than one proposal
		//public virtual Proposal Proposal { get; set; }
		public virtual ICollection<Proposal> Proposals { get; set; } //added

		public virtual ICollection<Notification> Notifications { get; set; }
        public virtual ICollection<Contract> Contracts { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
        public virtual ICollection<Project> Projects { get; set; }
		//after Review i think each user and skills has relation many to many so we need add new class
		//public virtual ICollection<Skill> Skills { get; set; }
		public virtual ICollection<UserSkill> UserSkills { get; set; } = new List<UserSkill>();


	}
}
