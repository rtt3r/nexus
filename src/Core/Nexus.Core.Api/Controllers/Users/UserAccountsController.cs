using Asp.Versioning;
using Goal.Application.Commands;
using Goal.Infra.Http.Controllers;
using Goal.Infra.Http.Controllers.Requests;
using Goal.Infra.Http.Controllers.Results;
using Goal.Infra.Http.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nexus.Core.Application.Commands.Users;
using Nexus.Core.Infra.Data.Query.Repositories.Users.Accounts;
using Nexus.Core.Model.Users;
using Nexus.Infra.Crosscutting;
using Nexus.Infra.Http.Controllers;

namespace Nexus.Core.Api.Controllers.Users;

[ApiController]
[ApiVersion("1")]
[Authorize]
[Route("v{version:apiVersion}/users")]
public class UserAccountsController(
    AppState appState,
    IMediator mediator,
    IUserAccountQueryRepository userAccountQueryRepository)
    : ApiControllerBase
{
    private readonly AppState appState = appState;
    private readonly IMediator mediator = mediator;
    private readonly IUserAccountQueryRepository userAccountQueryRepository = userAccountQueryRepository;

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ApiResponse))]
    public async Task<ActionResult<PagedResponse<UserAccount>>> Get([FromQuery] PageSearchRequest request)
        => Paged(await userAccountQueryRepository.QueryAsync(request.ToPageSearch()));

    [HttpGet("{id}", Name = $"{nameof(UserAccountsController)}_{nameof(GetById)}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ApiResponse))]
    [ProducesResponseType(StatusCodes.Status503ServiceUnavailable, Type = typeof(ApiResponse))]
    public async Task<ActionResult<UserAccount>> GetById([FromRoute] string id)
    {
        UserAccount? userAccount = await userAccountQueryRepository.LoadAsync(id);

        return userAccount is null
            ? NotFound()
            : Ok(userAccount);
    }

    [HttpGet("me", Name = $"{nameof(UserAccountsController)}_{nameof(GetMe)}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ApiResponse))]
    [ProducesResponseType(StatusCodes.Status503ServiceUnavailable, Type = typeof(ApiResponse))]
    public async Task<ActionResult<UserAccount>> GetMe()
    {
        UserAccount? userAccount = await userAccountQueryRepository.LoadAsync(appState.User.UserId!);

        if (userAccount is not null)
        {
            return Ok(userAccount);
        }

        var command = new CreateUserAccountCommand(
            appState.User.UserId!,
            appState.User.Email!,
            appState.User.Name!,
            appState.User.Username!
        );

        ICommandResult<UserAccount> result = await mediator.Send(command);

        return !result.IsSucceeded
            ? CommandFailure(result)
            : Ok(result.Data);
    }

    [HttpPatch("{id}")]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ApiResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse))]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ApiResponse))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ApiResponse))]
    public async Task<ActionResult<ApiResponse>> Patch([FromRoute] string id, [FromBody] UpdateUserProfileRequest request)
    {
        var command = new UpdateUserProfileCommand(
            id,
            request.Biography,
            request.Birthdate,
            request.Headline);

        ICommandResult result = await mediator.Send(command);

        return result.IsSucceeded
            ? AcceptedAtAction($"{nameof(UserAccountsController)}_{nameof(GetById)}", new { id }, null)
            : CommandFailure(result);
    }
}
