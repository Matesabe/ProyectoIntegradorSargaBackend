using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLogic.Exceptions.RepoException
{
    internal class GetByNameException : AppLogicException
    {
        public GetByNameException(string message) : base(message)
        {
        }
        public GetByNameException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
