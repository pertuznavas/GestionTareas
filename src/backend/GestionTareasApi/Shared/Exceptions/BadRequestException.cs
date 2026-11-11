namespace GestionTareasApi.Shared.Exceptions
{
    public class BadRequestException : AppException
    {
        public BadRequestException()
        {
        }

        public BadRequestException(int errorCode, string message) : base(errorCode, message)
        {
        }

        public BadRequestException(int errorCode, string? message, Exception? innerException) : base(errorCode, message, innerException)
        {
        }
    }
}
