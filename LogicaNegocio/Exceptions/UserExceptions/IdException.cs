namespace Libreria.LogicaNegocio.Excepciones.Usuario
{
    public class IdException : UserException
    {
        public IdException()
        {
        }

        public IdException(string? message) : base(message)
        {
        }
    }
}
