using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelanceBoard.Core.Domain.Entities
{
    public class Message
    {
        public int Id { get; set; }
        public string Body { get; set; }
        public bool IsRead { get; set; }
        public DateTime Timestamp { get; set; }
		public string? ReceiverId { get; set; }
		public string? SenderId { get; set; }

        // Navigation properties
		public virtual ApplicationUser? Sender { get; set; }
		public virtual ApplicationUser? Receiver { get; set; }

	}
}
