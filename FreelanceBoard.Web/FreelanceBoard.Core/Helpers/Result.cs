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
            => new(true, data, operation, message);

        public static Result<EType> Failure(string operation, string message, int statusCode = 500)
            => new(false, default!, operation, message)
            {
                StatusCode = statusCode
            };
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