using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLogic.Exceptions.RepoException
{
    internal class GetByIdException : AppLogicException
    {
        public GetByIdException(string message) : base(message)
        {
        }
        public GetByIdException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }   
   
}
