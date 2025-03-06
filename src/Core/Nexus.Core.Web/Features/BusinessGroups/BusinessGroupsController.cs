using Asp.Versioning;
using Goal.Infra.Http.Controllers;
using Goal.Infra.Http.Controllers.Requests;
using Goal.Infra.Http.Controllers.Results;
using Goal.Infra.Http.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nexus.Core.Infra.Data.Query.Repositories.BusinessGroups;
using Nexus.Core.Model.BusinessGroups;
using Nexus.Core.Web.Features.BusinessGroups.CreateBusinessGroup;
using Nexus.Core.Web.Features.BusinessGroups.UpdateBusinessGroup;
using Nexus.Infra.Crosscutting.Errors;
using Nexus.Infra.Http.Controllers;
using OneOf;
using OneOf.Types;

namespace Nexus.Core.Web.Features.BusinessGroups;

[ApiController]
[ApiVersion("1")]
[Authorize("admin")]
[Route("v{version:apiVersion}/[controller]")]
public class BusinessGroupsController(
    IBusinessGroupQueryRepository businessGroupQueryRepository,
    IMediator mediator)
    : NexusApiController
{
    private readonly IBusinessGroupQueryRepository businessGroupQueryRepository = businessGroupQueryRepository;
    private readonly IMediator mediator = mediator;

    private const string GET_BY_ID_ROUTE = $"{nameof(BusinessGroupsController)}.{nameof(GetById)}";

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ApiResponse))]
    public async Task<ActionResult<PagedResponse<BusinessGroup>>> Get([FromQuery] PageSearchRequest request)
        => Paged(await businessGroupQueryRepository.QueryAsync(request.ToPageSearch()));

    [HttpGet("{id}", Name = GET_BY_ID_ROUTE)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ApiResponse))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ApiResponse))]
    public async Task<ActionResult<BusinessGroup>> GetById([FromRoute] string id)
    {
        BusinessGroup? businessGroup = await businessGroupQueryRepository.LoadAsync(id);

        return businessGroup is null
            ? NotFound()
            : Ok(businessGroup);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse))]
    [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(ApiResponse))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ApiResponse))]
    public async Task<ActionResult<ApiResponse<BusinessGroup>>> Post([FromBody] CreateBusinessGroupRequest request)
    {
        OneOf<BusinessGroup, AppError> result = await mediator.Send<OneOf<BusinessGroup, AppError>>(request.ToCommand());

        return result
            .Match<ActionResult<ApiResponse<BusinessGroup>>>(
                businessGroup => CreatedAtRoute(
                    GET_BY_ID_ROUTE,
                    new { id = businessGroup.Id },
                    ApiResponse.Success(businessGroup)),
                error => Error(error)
            );
    }

    [HttpPatch("{id}")]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ApiResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse))]
    [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(ApiResponse))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ApiResponse))]
    public async Task<ActionResult<ApiResponse<BusinessGroup>>> Patch([FromRoute] string id, [FromBody] UpdateBusinessGroupRequest request)
    {
        OneOf<None, AppError> result = await mediator.Send<OneOf<None, AppError>>(request.ToCommand(id));

        return result
           .Match<ActionResult<ApiResponse<BusinessGroup>>>(
               none => AcceptedAtRoute(GET_BY_ID_ROUTE, new { id }, ApiResponse.Success()),
               error => Error(error)
           );
    }

    // [HttpDelete("{id}")]
    // [ProducesResponseType(StatusCodes.Status202Accepted)]
    // [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ApiResponse))]
    // [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse))]
    // [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ApiResponse))]
    // [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ApiResponse))]
    // public async Task<ActionResult<ApiResponse>> Delete([FromRoute] string id)
    // {
    //     OneOf<None, AppError> result = await mediator.Send<OneOf<None, AppError>>(new RemoveBusinessGroupCommand(id));

    //     return result
    //        .Match<ActionResult<ApiResponse>>(
    //            none => Accepted(ApiResponse.Success()),
    //            error => Error(error)
    //        );
    // }
}
