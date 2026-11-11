namespace GestionTareasApi.Shared.Exceptions;

public class NotFoundException : AppException
{
    public NotFoundException()
    {
    }

    public NotFoundException(int errorCode, string message) : base(message)
    {
    }

    public NotFoundException(int errorCode, string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
