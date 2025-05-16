using Libreria.LogicaNegocio.Excepciones.Usuario;

namespace Libreria.LogicaNegocio.Excepciones
{
    public class EmailException : UserException
    {
        public EmailException()
        {
        }

        public EmailException(string? message) : base(message)
        {
        }
    }
}
