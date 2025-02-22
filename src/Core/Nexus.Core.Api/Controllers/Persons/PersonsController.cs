using Asp.Versioning;
using Goal.Infra.Http.Controllers;
using Goal.Infra.Http.Controllers.Requests;
using Goal.Infra.Http.Controllers.Results;
using Goal.Infra.Http.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nexus.Core.Application.Persons.Commands;
using Nexus.Core.Infra.Data.Query.Repositories.Persons;
using Nexus.Core.Model.Persons;
using Nexus.Infra.Crosscutting.Errors;
using Nexus.Infra.Http.Controllers;
using OneOf;
using OneOf.Types;

namespace Nexus.Core.Api.Controllers.Persons;

[ApiController]
[ApiVersion("1")]
[Authorize("admin")]
[Route("v{version:apiVersion}/[controller]")]
public class PersonsController(
    IPersonQueryRepository personQueryRepository,
    IMediator mediator)
    : NexusApiController
{
    private readonly IPersonQueryRepository personQueryRepository = personQueryRepository;
    private readonly IMediator mediator = mediator;

    private const string GET_BY_ID_ROUTE = $"{nameof(PersonsController)}.{nameof(GetById)}";

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ApiResponse))]
    public async Task<ActionResult<PagedResponse<NaturalPerson>>> Get([FromQuery] PageSearchRequest request)
        => Paged(await personQueryRepository.QueryAsync(request.ToPageSearch()));

    [HttpGet("{id}", Name = GET_BY_ID_ROUTE)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ApiResponse))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ApiResponse))]
    public async Task<ActionResult<NaturalPerson>> GetById([FromRoute] string id)
    {
        NaturalPerson? person = await personQueryRepository.LoadAsync(id);

        return person is null
            ? NotFound()
            : Ok(person);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse))]
    [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(ApiResponse))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ApiResponse))]
    public async Task<ActionResult<ApiResponse<NaturalPerson>>> Post([FromBody] RegisterPersonRequest request)
    {
        OneOf<NaturalPerson, AppError> result = await mediator.Send<OneOf<NaturalPerson, AppError>>(request.ToCommand());

        return result
            .Match<ActionResult<ApiResponse<NaturalPerson>>>(
                person => CreatedAtRoute(GET_BY_ID_ROUTE, new { id = person.Id }, ApiResponse.Success(person)),
                error => Error(error)
            );
    }

    [HttpPatch("{id}")]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ApiResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse))]
    [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(ApiResponse))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ApiResponse))]
    public async Task<ActionResult<ApiResponse<NaturalPerson>>> Patch([FromRoute] string id, [FromBody] UpdatePersonRequest request)
    {
        OneOf<None, AppError> result = await mediator.Send<OneOf<None, AppError>>(request.ToCommand(id));

        return result
           .Match<ActionResult<ApiResponse<NaturalPerson>>>(
               none => AcceptedAtRoute(GET_BY_ID_ROUTE, new { id }, ApiResponse.Success()),
               error => Error(error)
           );
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ApiResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse))]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ApiResponse))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ApiResponse))]
    public async Task<ActionResult<ApiResponse>> Delete([FromRoute] string id)
    {
        OneOf<None, AppError> result = await mediator.Send<OneOf<None, AppError>>(new RemovePersonCommand(id));

        return result
           .Match<ActionResult<ApiResponse>>(
               none => Accepted(ApiResponse.Success()),
               error => Error(error)
           );
    }
}
