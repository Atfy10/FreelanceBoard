using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelanceBoard.Core.Domain.Entities
{
	public class Project
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public string Attachments { get; set; }
		public string UserId { get; set; }

		// Navigation property
		public virtual ApplicationUser User { get; set; }
	}

}
