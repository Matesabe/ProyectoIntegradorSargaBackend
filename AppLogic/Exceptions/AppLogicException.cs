using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLogic.Exceptions
{
    public class AppLogicException : Exception
    {
        public AppLogicException(string message) : base(message)
        {
        }
        public AppLogicException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
