using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelanceBoard.Core.Exceptions
{
	public class UserNameExistException : Exception
	{
		public UserNameExistException(string message) : base(message)
		{
		}

	}
}
