using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreelanceBoard.Core.Domain.Enums;

namespace FreelanceBoard.Core.Domain.Entities
{
	public class Proposal
	{
		public int Id { get; set; }
		public string Message { get; set; }
		public string Status { get; set; }
		public decimal Price { get; set; }

		public string FreelancerId { get; set; }
		public int JobId { get; set; }

		// Navigation properties
		public virtual ApplicationUser Freelancer { get; set; }
		public virtual Job Job { get; set; }
	}

}
