using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelanceBoard.Core.Exceptions
{
	public class PhoneExistException : Exception
	{
		public PhoneExistException(string message) : base(message)
		{
		}
	}
}
