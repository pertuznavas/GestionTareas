using GestionTareasApi.Shared.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;

namespace GestionTareasApi.Configurations.Middlewares;

public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        var (statusCode, title) = exception switch
        {
            BadRequestException => (HttpStatusCode.BadRequest, "Bad request"),
            NotFoundException => (HttpStatusCode.NotFound, "Not found"),
            AppException => (HttpStatusCode.Conflict, "Operation not allowed"),
            _ => (HttpStatusCode.InternalServerError, "Internal server error")
        };

        var isServerError = statusCode == HttpStatusCode.InternalServerError;

        if (isServerError)
        {
            logger.LogError(exception, "Unhandled exception in {Path}", httpContext.Request.Path);
        }
        else
        {
            logger.LogWarning("Exception {Type} in {Path}: {Message}", exception.GetType().Name, httpContext.Request.Path, exception.Message);
        }

        var error = new
        {
            Title = title,
            ErrorCode = exception is AppException appEx ? appEx.ErrorCode : 0,
            ErrorMessage = isServerError ? "An unexpected error occurred. Please try again later." : exception.Message,
        };

        httpContext.Response.StatusCode = (int)statusCode;
        await httpContext.Response.WriteAsJsonAsync(error, cancellationToken);

        return true;
    }
}
