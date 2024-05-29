using Goal.Infra.Http.Controllers.Results;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Nexus.Infra.Crosscutting.Constants;
using Nexus.Infra.Http.Controllers;

namespace Nexus.Infra.Http.Filters;

public class HttpExceptionFilter(ILogger<HttpExceptionFilter> logger) : IExceptionFilter
{
    private readonly ILogger<HttpExceptionFilter> logger = logger;

    public void OnException(ExceptionContext context)
    {
        logger.LogError(context.Exception, ApplicationConstants.Messages.UNEXPECTED_ERROR);

        var response = ApiResponse.Fail(
            new ApiResponseMessage(
                nameof(ApplicationConstants.Messages.UNEXPECTED_ERROR),
                ApplicationConstants.Messages.UNEXPECTED_ERROR));

        context.Result = new InternalServerErrorObjectResult(response);
        context.ExceptionHandled = true;
    }
}
