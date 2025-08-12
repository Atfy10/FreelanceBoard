using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelanceBoard.Core.Exceptions
{
    public class InvalidSortChoiceException : Exception
    {
        public InvalidSortChoiceException(string message) : base(message)
        {
        }
        public InvalidSortChoiceException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
