using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLogic.Exceptions.RepoException
{
    internal class GetAllException : AppLogicException
    {
        public GetAllException(string message) : base(message)
        {
        }
        public GetAllException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
