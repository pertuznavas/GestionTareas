namespace GestionTareasApi.Shared.Exceptions;

public class ConflictException : AppException
{
    public ConflictException()
    {
    }

    public ConflictException(string message) : base(message)
    {
    }

    public ConflictException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
