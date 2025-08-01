﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelanceBoard.Core.Domain.Entities
{
	public class Payement
	{
		public string PaymentNumber { get; set; }
		public decimal Amount { get; set; }
		public string Status { get; set; }
		public DateTime Date { get; set; }

		// Navigation properties
		public virtual Contract Contract { get; set; }
	}


}
