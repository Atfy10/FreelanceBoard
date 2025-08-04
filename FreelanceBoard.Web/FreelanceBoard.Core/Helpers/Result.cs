using FreelanceBoard.Core.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace FreelanceBoard.Core.Helpers
{
    public class Result<EType> 
    {
        public bool IsSuccess { get; set; }
        public string Operation { get; set; }
        public string Message { get; set; }
        public EType? Data { get; set; }
        
        public static Result<EType> Success(EType data, string operation, string message = "Operation done successfully")
        {
            return new Result<EType>
            {
                IsSuccess = true,
                Data = data,
                Operation = operation,
                Message = message
            };
		}

        public static Result<EType> Failure(string operation, string message)
        {
            return new Result<EType>
            {
                IsSuccess = false,
                Operation = operation,
                Message = message
            };
        }
        
        



	}
}
