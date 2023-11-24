using Nexus.Infra.Http.Controllers;
using Goal.Seedwork.Infra.Http.Controllers;
using Goal.Seedwork.Infra.Http.Controllers.Requests;
using Goal.Seedwork.Infra.Http.Controllers.Results;
using Goal.Seedwork.Infra.Http.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nexus.Infra.Crosscutting;
using Nexus.Core.Model.Users;
using Nexus.Core.Infra.Data.Query.Repositories.Users.Accounts;
using Goal.Seedwork.Application.Commands;
using Nexus.Core.Application.Commands.Users;

namespace Nexus.Core.Api.Controllers.Users;

[ApiController]
[ApiVersion("1")]
[Authorize]
[Route("v{version:apiVersion}/users")]
public class UserAccountsController : ApiControllerBase
{
    private readonly AppState appState;
    private readonly IMediator mediator;
    private readonly IUserAccountQueryRepository userAccountQueryRepository;

    public UserAccountsController(
        AppState appState,
        IMediator mediator,
        IUserAccountQueryRepository userAccountQueryRepository)
    {
        this.userAccountQueryRepository = userAccountQueryRepository;
        this.mediator = mediator;
        this.appState = appState;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ApiResponse))]
    public async Task<ActionResult<PagedResponse<UserAccount>>> Get([FromQuery] PageSearchRequest request)
        => Paged(await userAccountQueryRepository.QueryAsync(request.ToPageSearch()));

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ApiResponse))]
    [ProducesResponseType(StatusCodes.Status503ServiceUnavailable, Type = typeof(ApiResponse))]
    public async Task<ActionResult<UserAccount>> GetById([FromRoute] string id)
    {
        var userAccount = await userAccountQueryRepository.LoadAsync(id);

        return userAccount is null
            ? NotFound()
            : Ok(userAccount);
    }

    [HttpGet("me")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ApiResponse))]
    [ProducesResponseType(StatusCodes.Status503ServiceUnavailable, Type = typeof(ApiResponse))]
    public async Task<ActionResult<UserAccount>> GetMe()
    {
        var userAccount = await userAccountQueryRepository.LoadAsync(appState.User.UserId);

        if (userAccount is not null)
            return Ok(userAccount);

        var command = new CreateUserAccountCommand
        {
            Id = appState.User.UserId,
            Email = appState.User.Email,
            Name = appState.User.Name,
            Username = appState.User.Username
        };

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
    public async Task<ActionResult> Patch([FromRoute] string id, [FromBody] UpdateUserProfileRequest request)
    {
        var command = new UpdateUserProfileCommand
        {
            Id = id,
            Biography = request.Biography,
            Birthdate = request.Birthdate,
            Headline = request.Headline
        };

        ICommandResult result = await mediator.Send(command);

        return result.IsSucceeded
            ? AcceptedAtAction($"{nameof(UserAccountsController)}_{nameof(GetById)}", new { id }, null)
            : CommandFailure(result);
    }
}
