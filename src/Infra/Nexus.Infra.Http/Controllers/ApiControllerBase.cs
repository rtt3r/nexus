using Goal.Application.Commands;
using Goal.Infra.Http.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Nexus.Infra.Http.Controllers;

public class ApiControllerBase : ApiController
{
    protected override ActionResult CommandFailure(ICommandResult result)
    {
        if (result.HasInputValidation)
        {
            return BadRequest(ApiResponse.FromCommand(result));
        }

        if (result.HasResourceNotFound)
        {
            return NotFound(ApiResponse.FromCommand(result));
        }

        if (result.HasDomainViolation)
        {
            return UnprocessableEntity(ApiResponse.FromCommand(result));
        }

        return result.HasExternalError
            ? ServiceUnavailable(ApiResponse.FromCommand(result))
            : InternalServerError(ApiResponse.FromCommand(result));
    }
}
