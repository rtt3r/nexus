using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Nexus.Infra.Crosscutting.Exceptions;
using Nexus.Infra.Crosscutting.Notifications;
using Nexus.Infra.Http.Controllers;
using static Nexus.Infra.Crosscutting.Constants.ApplicationConstants;

namespace Nexus.Infra.Http.Handlers.Exceptions;

public abstract class NexusExceptionHandler<TException>(ILogger<NexusExceptionHandler<TException>> logger) : IExceptionHandler
    where TException : NexusException
{
    protected readonly ILogger<NexusExceptionHandler<TException>> _logger = logger;

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        if (exception is not TException e)
        {
            return false;
        }

        httpContext.Response.StatusCode = (int)e.StatusCode;

        await httpContext.Response.WriteAsJsonAsync(
            await HandleResponseAsync(httpContext, e, cancellationToken),
            cancellationToken);

        return true;
    }

    protected virtual async Task<ApiResponse> HandleResponseAsync(HttpContext httpContext, TException exception, CancellationToken cancellationToken)
    {
        _logger.LogError(exception, Messages.UNEXPECTED_ERROR);
        await Task.CompletedTask;

        return ApiResponse.Fail(
            exception.Notifications
                .Prepend(new Notification(nameof(Messages.UNEXPECTED_ERROR), Messages.UNEXPECTED_ERROR))
                .ToArray()
        );
    }
}


