using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Nexus.Infra.Crosscutting.Constants;
using Nexus.Infra.Crosscutting.Exceptions;
using Nexus.Infra.Http.Controllers;

namespace Nexus.Infra.Http.Handlers.Exceptions;

public sealed class ResourceNotFoundExceptionHandler(ILogger<ResourceNotFoundExceptionHandler> logger)
    : NexusExceptionHandler<ResourceNotFoundException>(logger)
{
    protected override async Task<ApiResponse> HandleResponseAsync(HttpContext httpContext, ResourceNotFoundException exception, CancellationToken cancellationToken)
    {
        logger.LogError(exception, ApplicationConstants.Messages.RESOURCE_NOT_FOUND);
        await Task.CompletedTask;

        return ApiResponse.Fail(exception.Notifications);
    }
}
