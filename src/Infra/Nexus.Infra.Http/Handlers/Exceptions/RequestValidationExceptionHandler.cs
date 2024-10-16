using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Nexus.Infra.Crosscutting.Constants;
using Nexus.Infra.Crosscutting.Exceptions;
using Nexus.Infra.Http.Controllers;

namespace Nexus.Infra.Http.Handlers.Exceptions;

public sealed class RequestValidationExceptionHandler(ILogger<RequestValidationExceptionHandler> logger)
    : NexusExceptionHandler<RequestValidationException>(logger)
{
    protected override async Task<ApiResponse> HandleResponseAsync(HttpContext httpContext, RequestValidationException exception, CancellationToken cancellationToken)
    {
        logger.LogError(exception, ApplicationConstants.Messages.REQUEST_VALIDATION);
        await Task.CompletedTask;

        return ApiResponse.Fail(exception.Notifications);
    }
}
