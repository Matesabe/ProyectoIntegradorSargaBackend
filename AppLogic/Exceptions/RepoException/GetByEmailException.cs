using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLogic.Exceptions.RepoException
{
    public class GetByEmailException : AppLogicException
    {
        public GetByEmailException(string message) : base(message)
        {
        }
        public GetByEmailException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
