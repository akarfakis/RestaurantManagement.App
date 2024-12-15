using Microsoft.AspNetCore.Diagnostics;
using RestaurantManagement.Common.Exceptions;
using System.Diagnostics;

namespace RestaurantManagement.Api.Handlers;

public class GlobalExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        var logger = httpContext.RequestServices.GetRequiredService<ILogger<Program>>();
        var statusCode = exception switch
        {
            InvalidOperationException => StatusCodes.Status400BadRequest,
            BaseException baseException => baseException.StatusCode,
            InvalidInputException => StatusCodes.Status400BadRequest,
            _ => StatusCodes.Status500InternalServerError
        };
        logger.LogError(
                exception,
                "Could not process a request on machine {Machine}. TraceId: {TraceId}",
                Environment.MachineName,
                Activity.Current?.Id);

        await Results.Problem(
            title: exception.Message,
            statusCode: statusCode,
            extensions: new Dictionary<string, object?>
            {
               {"traceId", Activity.Current?.Id }
            }).ExecuteAsync(httpContext);

        return true;
    }
}
