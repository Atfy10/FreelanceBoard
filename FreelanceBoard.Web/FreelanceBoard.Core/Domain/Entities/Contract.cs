﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreelanceBoard.Core.Domain.Enums;


namespace FreelanceBoard.Core.Domain.Entities
{
	public class Contract
	{
		public int Id { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public decimal Price { get; set; }
		public string Status { get; set; }

		public string UserId { get; set; }
		public int JobId { get; set; }
		public string PaymentNumber { get; set; }

		// Navigation properties
		public virtual Job Job { get; set; }
		public virtual Payement Payment { get; set; }
		public virtual ApplicationUser User { get; set; }
		public virtual ICollection<Review> Reviews { get; set; } = [];
	}

}
