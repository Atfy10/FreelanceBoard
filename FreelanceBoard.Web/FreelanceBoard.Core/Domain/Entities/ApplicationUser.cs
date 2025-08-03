using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using FreelanceBoard.Core.Dtos;
using Microsoft.AspNetCore.Identity;

namespace FreelanceBoard.Core.Domain.Entities
{
	public class ApplicationUser : IdentityUser
	{
		public string FirstName { get;  set; }
		public string LastName { get;  set; }
		public bool IsBanned { get;  set; }

		// Navigation properties 
		public virtual ICollection<Job> Jobs { get; private set; } = [];
		public virtual Profile Profile { get; private set; }
		public virtual ICollection<Proposal> Proposals { get; private set; } = [];
		public virtual ICollection<Notification> Notifications { get; private set; } = [];
		public virtual ICollection<Contract> Contracts { get; private set; } = [];
		public virtual ICollection<Message> SentMessages { get; private set; } = [];
		public virtual ICollection<Message> ReceivedMessages { get; private set; } = [];
		public virtual ICollection<Project> Projects { get; private set; } = [];
		public virtual ICollection<Skill> Skills { get; private set; } = [];

		public void UpdateDetails(string firstName,string lastName,string email,string phoneNumber,string userName,bool isBanned)
		{
			FirstName = firstName;
			LastName = lastName;
			Email = email;
			PhoneNumber = phoneNumber;
			UserName = userName;
			IsBanned = isBanned;
		}

	}

}
