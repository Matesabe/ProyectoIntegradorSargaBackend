
namespace Libreria.LogicaNegocio.Excepciones.Usuario
{
    public class UserException : BusinessLogicExpception
    {
        public UserException()
        {
        }

        public UserException(string? message) : base(message)
        {
        }
    }
}
