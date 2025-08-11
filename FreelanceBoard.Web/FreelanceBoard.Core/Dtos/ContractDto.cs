using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelanceBoard.Core.Dtos
{
    public class ContractDto
    {

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal Price { get; set; }
        public string Status { get; set; }

        public string? PaymentNumber { get; set; }


    }
}
