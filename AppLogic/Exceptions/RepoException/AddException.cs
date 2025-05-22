using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLogic.Exceptions.RepoException
{
    internal class AddException : AppLogicException
    {
        public AddException(string message) : base(message)
        {
        }
        public AddException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }

}


