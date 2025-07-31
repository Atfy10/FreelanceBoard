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
		public virtual ICollection<Proposal> Proposals { get; set; } //added
		public virtual ICollection<Notification> Notifications { get; set; }
        public virtual ICollection<Contract> Contracts { get; set; }
		public virtual ICollection<Message> SentMessages { get; set; } = new List<Message>();
		public virtual ICollection<Message> ReceivedMessages { get; set; } = new List<Message>();
		public virtual ICollection<Project> Projects { get; set; }
		public virtual ICollection<Skill> Skills { get; set; }
	}
}
