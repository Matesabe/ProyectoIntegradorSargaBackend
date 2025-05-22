using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLogic.Exceptions.RepoException
{
    internal class DeleteException : AppLogicException
    {
        public DeleteException(string message) : base(message)
        {
        }
        public DeleteException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
