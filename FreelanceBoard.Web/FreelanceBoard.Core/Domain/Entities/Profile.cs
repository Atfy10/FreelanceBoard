using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelanceBoard.Core.Domain.Entities
{
	public class Profile
	{
		public string UserId { get; set; }
		public string Bio { get; set; }
		public string Image { get; set; }

		// Navigation property
		public virtual ApplicationUser User { get; set; }
	}


}
