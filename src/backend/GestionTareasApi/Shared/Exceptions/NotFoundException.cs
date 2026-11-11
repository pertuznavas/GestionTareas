namespace GestionTareasApi.Shared.Exceptions
{
    public class NotFoundException : AppException
    {
        public NotFoundException()
        {
        }

        public NotFoundException(int errorCode, string message) : base(errorCode, message)
        {
        }

        public NotFoundException(int errorCode, string? message, Exception? innerException) : base(errorCode, message, innerException)
        {
        }
    }
}
