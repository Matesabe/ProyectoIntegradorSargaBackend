using System.Runtime.Serialization;

namespace Infrastructure.DataAccess.EF.Exceptions
{
    public class BadRequestException : InfrastructuraException
    {
        public BadRequestException()
        {
        }

        public BadRequestException(string? message) : base(message)
        {
        }

        protected BadRequestException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public override int statusCode()
        {
            return 400;
        }
    }
}
