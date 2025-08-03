using FreelanceBoard.Core.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace FreelanceBoard.Core.Domain
{
    public class Result<EType> where EType : class
    {
        public bool IsSuccess { get; set; }
        public OperationType Operation { get; set; }
        public string Message { get; set; }
        public EType Data { get; set; }
        public EType? OldData { get; set; }

    }
}
