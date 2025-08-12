using FreelanceBoard.Core.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace FreelanceBoard.Core.Helpers
{
    public class Result<EType> : ResultBase
    {
        public EType? Data { get; set; }

        private Result(bool isSuccess, EType data, string operationType, string message)
                : base(isSuccess, operationType, message)
        {
            Data = data;
        }
        public static Result<EType> Success(EType data, string operation, string message = "Operation done successfully")
            => new Result<EType>(true, data, operation, message);
        //      {
        //          return new Result<EType>
        //          {
        //              IsSuccess = true,
        //              Data = data,
        //              Operation = operation,
        //              Message = message
        //          };
        //}

        public static Result<EType> Failure(string operation, string message)
            => new Result<EType>(false, default!, operation, message);
        //{
        //return new Result<EType>
        //    {
        //        IsSuccess = false,
        //        Operation = operation,
        //        Message = message
        //    };
    }

    public class Result : ResultBase
    {
        public Result(bool isSuccess, string operationType, string message)
            : base(isSuccess, operationType, message)
        {
        }
        public static Result Success(string operation, string message = "Operation done successfully")
            => new Result(true, operation, message);
        public static Result Failure(string operation, string message)
            => new Result(false, operation, message);
    }
}