using Libreria.LogicaNegocio.Excepciones.Usuario;

namespace Libreria.LogicaNegocio.Excepciones
{
    public class EmailRepetidoException : EmailException
    {
        public EmailRepetidoException()
        {
        }
                public EmailRepetidoException(string? message) : base(message)
        {
        }
    }
}
