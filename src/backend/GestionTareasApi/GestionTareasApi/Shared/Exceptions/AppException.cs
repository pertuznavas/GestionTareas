namespace GestionTareasApi.Shared.Exceptions
{
    public class AppException : Exception
    {
        public int ErrorCode { get; private set; }

        public AppException()
        {
            
        }
        public AppException(int errorCode, string message):base(message)
        {
            ErrorCode = errorCode;
        }

        public AppException(int errorCode, string? message, Exception? innerException) : base(message, innerException)
        {
            ErrorCode = errorCode;
        }
    }
}
