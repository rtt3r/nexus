using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Nexus.Infra.Crosscutting.Constants;
using Nexus.Infra.Crosscutting.Exceptions;
using Nexus.Infra.Crosscutting.Notifications;
using Nexus.Infra.Http.Controllers;

namespace Nexus.Infra.Http.Handlers.Exceptions;

public sealed class ServiceUnavailableExceptionHandler(ILogger<ServiceUnavailableExceptionHandler> logger)
    : NexusExceptionHandler<ServiceUnavailableException>(logger)
{
    protected override async Task<ApiResponse> HandleResponseAsync(HttpContext httpContext, ServiceUnavailableException exception, CancellationToken cancellationToken)
    {
        logger.LogError(exception, ApplicationConstants.Messages.SERVICE_UNAVAILABLE);

        await Task.CompletedTask;

        return ApiResponse.Fail(
            new Notification(nameof(ApplicationConstants.Messages.SERVICE_UNAVAILABLE), ApplicationConstants.Messages.SERVICE_UNAVAILABLE)
        );
    }
}
