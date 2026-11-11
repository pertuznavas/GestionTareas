namespace GestionTareasApi.Shared.Exceptions
{
    public class AppException : Exception
    {
        public int ErrorCode { get; private set; }

        public AppException()
        {
            
        }
        public AppException(int erroCode, string message):base(message)
        {
            ErrorCode = erroCode;
        }

        public AppException(int erroCode, string? message, Exception? innerException) : base(message, innerException)
        {
            ErrorCode = erroCode;
        }
    }
}
