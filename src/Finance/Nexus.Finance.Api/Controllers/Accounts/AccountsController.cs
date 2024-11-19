using Asp.Versioning;
using Goal.Infra.Http.Controllers;
using Goal.Infra.Http.Controllers.Requests;
using Goal.Infra.Http.Controllers.Results;
using Goal.Infra.Http.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nexus.Finance.Application.Accounts.Commands;
using Nexus.Finance.Infra.Data.Query.Repositories.Accounts;
using Nexus.Finance.Model.Accounts;
using Nexus.Infra.Crosscutting.Errors;
using Nexus.Infra.Http.Controllers;
using OneOf;
using OneOf.Types;

namespace Nexus.Finance.Api.Controllers.Accounts;

[ApiController]
[ApiVersion("1")]
[Authorize("admin")]
[Route("v{version:apiVersion}/[controller]")]
internal class AccountsController(
    IAccountQueryRepository accountQueryRepository,
    IMediator mediator)
    : NexusApiController
{
    private readonly IAccountQueryRepository accountQueryRepository = accountQueryRepository;
    private readonly IMediator mediator = mediator;

    private const string GET_BY_ID_ROUTE = $"{nameof(AccountsController)}.{nameof(GetById)}";

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ApiResponse))]
    public async Task<ActionResult<PagedResponse<Account>>> Get([FromQuery] PageSearchRequest request)
        => Paged(await accountQueryRepository.QueryAsync(request.ToPageSearch()));

    [HttpGet("{id}", Name = GET_BY_ID_ROUTE)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ApiResponse))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ApiResponse))]
    public async Task<ActionResult<Account>> GetById([FromRoute] string id)
    {
        Account? account = await accountQueryRepository.LoadAsync(id);

        return account is null
            ? NotFound()
            : Ok(account);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse))]
    [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(ApiResponse))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ApiResponse))]
    public async Task<ActionResult<ApiResponse<Account>>> Post([FromBody] RegisterAccountRequest request)
    {
        OneOf<Account, AppError> result = await mediator.Send<OneOf<Account, AppError>>(request.ToCommand());

        return result
            .Match<ActionResult<ApiResponse<Account>>>(
                account => CreatedAtRoute(GET_BY_ID_ROUTE, new { id = account.AccountId }, ApiResponse.Success(account)),
                error => Error(error)
            );
    }

    [HttpPatch("{id}")]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ApiResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse))]
    [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(ApiResponse))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ApiResponse))]
    public async Task<ActionResult<ApiResponse<Account>>> Patch([FromRoute] string id, [FromBody] UpdateAccountRequest request)
    {
        OneOf<None, AppError> result = await mediator.Send<OneOf<None, AppError>>(request.ToCommand(id));

        return result
           .Match<ActionResult<ApiResponse<Account>>>(
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
        OneOf<None, AppError> result = await mediator.Send<OneOf<None, AppError>>(new RemoveAccountCommand(id));

        return result
           .Match<ActionResult<ApiResponse>>(
               none => Accepted(ApiResponse.Success()),
               error => Error(error)
           );
    }
}
