using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Nexus.Infra.Crosscutting.Constants;
using Nexus.Infra.Http.Controllers;

namespace Nexus.Infra.Http.Handlers;

public sealed class NexusExceptionHandler(ILogger<NexusExceptionHandler> logger) : IExceptionHandler
{
    private readonly ILogger<NexusExceptionHandler> _logger = logger;

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        _logger.LogError(exception, "An unexpected problem has occurred: {InformationData}", exception.Message);

        httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

        await httpContext.Response.WriteAsJsonAsync(
            ApiResponse.Fail(Notifications.Shared.UnexpectedError(exception.Message)),
            cancellationToken);

        return true;
    }
}


