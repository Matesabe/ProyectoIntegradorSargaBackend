using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DataAccess.EF.Exceptions
{
    public class NotEnoughPointsException : Exception
    {
        public NotEnoughPointsException() { }
        public NotEnoughPointsException(string? message) : base(message) { }
    }
}
