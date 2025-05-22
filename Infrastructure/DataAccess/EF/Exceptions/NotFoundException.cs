using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DataAccess.EF.Exceptions
{
    public class NotFoundException : InfrastructuraException
    {
        public NotFoundException()
        {
        }

        public NotFoundException(string? message) : base(message)
        {
        }

        public override int statusCode()
        {
            return 404;
        }
    }
}
