using FreelanceBoard.Core.Dtos;
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

		public void UpdateDetails(UserUpdateDto userDto)
		{
			if (userDto == null)
			{
				throw new ArgumentNullException(nameof(userDto), "User update data cannot be null.");
			}
			if (string.IsNullOrWhiteSpace(userDto.Id) || userDto.Id != Id)
			{
				throw new ArgumentException("Invalid user ID.", nameof(userDto.Id));
			}
			FirstName = userDto.FirstName;
			LastName = userDto.LastName;
			Email = userDto.Email;
			PhoneNumber = userDto.PhoneNumber;
			UserName = userDto.UserName;
			IsBanned = userDto.IsBanned;
		}

	}

}
