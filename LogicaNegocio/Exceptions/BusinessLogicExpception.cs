
namespace Libreria.LogicaNegocio.Excepciones
{
    public abstract class BusinessLogicExpception : Exception
    {
        public BusinessLogicExpception()
        {
        }

        public BusinessLogicExpception(string? message) : base(message)
        {
        }

        public BusinessLogicExpception(string? message, Exception? innerException) : base(message)
        {
        }
    }
}
