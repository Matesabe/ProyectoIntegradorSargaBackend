using System.Runtime.Serialization;

namespace Infrastructure.DataAccess.EF.Exceptions
{
    public abstract class InfrastructuraException : Exception
    {
        string _message;
        public InfrastructuraException()
        {
        }

        public InfrastructuraException(string? message) : base(message)
        {
            _message = message;
        }

        protected InfrastructuraException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public abstract int statusCode();

        public Error Error()
        {
            return new Error(
                statusCode(),
                _message
                );

        }

    }
}
