using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLogic.Exceptions.RepoException
{
    internal class UpdateException : AppLogicException
    {
        public UpdateException(string message) : base(message)
        {
        }
        public UpdateException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
