using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Nexus.Infra.Crosscutting.Exceptions;
using Nexus.Infra.Crosscutting.Notifications;
using Nexus.Infra.Http.Controllers;
using static Nexus.Infra.Crosscutting.Constants.ApplicationConstants;

namespace Nexus.Infra.Http.Handlers.Exceptions;

public sealed class InternalServerErrorExceptionHandler(ILogger<InternalServerErrorExceptionHandler> logger)
    : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        logger.LogError(exception, Messages.UNEXPECTED_ERROR);

        httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

        ApiResponse response = exception is not InternalServerErrorException e
            ? ApiResponse.Fail(new Notification(nameof(Messages.UNEXPECTED_ERROR), Messages.UNEXPECTED_ERROR))
            : ApiResponse.Fail(
                e.Notifications
                    .Prepend(new Notification(nameof(Messages.UNEXPECTED_ERROR), Messages.UNEXPECTED_ERROR))
                    .ToArray()
            );

        await httpContext.Response.WriteAsJsonAsync(response, cancellationToken);

        return true;
    }
}