using Libreria.LogicaNegocio.Excepciones;
using Libreria.LogicaNegocio.Excepciones.Usuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Exceptions.UserExceptions
{
    public class NameException : UserException
    {
        public NameException() { }
        public NameException(string? message) : base(message) { }
    }
}
