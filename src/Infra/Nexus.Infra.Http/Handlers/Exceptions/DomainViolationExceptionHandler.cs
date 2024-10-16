using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Nexus.Infra.Crosscutting.Constants;
using Nexus.Infra.Crosscutting.Exceptions;
using Nexus.Infra.Http.Controllers;

namespace Nexus.Infra.Http.Handlers.Exceptions;

public sealed class DomainViolationExceptionHandler(ILogger<DomainViolationExceptionHandler> logger)
    : NexusExceptionHandler<DomainViolationException>(logger)
{
    protected override async Task<ApiResponse> HandleResponseAsync(HttpContext httpContext, DomainViolationException exception, CancellationToken cancellationToken)
    {
        logger.LogError(exception, ApplicationConstants.Messages.DOMAIN_VIOLATION);
        await Task.CompletedTask;

        return ApiResponse.Fail(exception.Notifications);
    }
}
