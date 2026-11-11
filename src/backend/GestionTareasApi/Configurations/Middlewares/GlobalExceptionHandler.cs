using GestionTareasApi.Shared.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace GestionTareasApi.Configurations.Middlewares;

/// <summary>
/// Turns exceptions into RFC 9457 <see cref="ProblemDetails"/> responses.
/// Unexpected errors return a generic message so internal details are not exposed.
/// </summary>
public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger, IProblemDetailsService problemDetailsService) : IExceptionHandler
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

        httpContext.Response.StatusCode = (int)statusCode;

        return await problemDetailsService.TryWriteAsync(new ProblemDetailsContext
        {
            HttpContext = httpContext,
            ProblemDetails = new ProblemDetails
            {
                Status = (int)statusCode,
                Title = title,
                Detail = isServerError ? "An unexpected error occurred. Please try again later." : exception.Message,
            },
        });
    }
}
