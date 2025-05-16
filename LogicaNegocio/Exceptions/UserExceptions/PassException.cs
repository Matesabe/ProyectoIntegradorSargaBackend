using Libreria.LogicaNegocio.Excepciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Exceptions.UserExceptions
{
    public class PassException : BusinessLogicExpception
    {
        public PassException() { }
        public PassException(string? message) : base(message) { }
    }
}
