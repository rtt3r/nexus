using Goal.Infra.Http.Controllers;
using Microsoft.AspNetCore.Mvc;
using Nexus.Infra.Crosscutting.Errors;

namespace Nexus.Infra.Http.Controllers;

public class NexusApiController : ApiController
{
    protected ActionResult Error(AppError error)
    {
        var responseMap = new Dictionary<Type, Func<ActionResult>>
        {
            { typeof(BusinessRuleError), () => Conflict(ApiResponse.Fail(error.Notifications)) },
            { typeof(InputValidationError), () => BadRequest(ApiResponse.Fail(error.Notifications)) },
            { typeof(ResourceNotFoundError), () => NotFound(ApiResponse.Fail(error.Notifications)) }
        };

        return responseMap.TryGetValue(error.GetType(), out Func<ActionResult>? response)
            ? response.Invoke()
            : InternalServerError(ApiResponse.Fail(error.Notifications));
    }
}
