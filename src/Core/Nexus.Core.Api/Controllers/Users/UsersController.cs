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
public class UsersController(
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
    public async Task<ActionResult<PagedResponse<User>>> Get([FromQuery] PageSearchRequest request)
        => Paged(await userAccountQueryRepository.QueryAsync(request.ToPageSearch()));

    [HttpGet("{id}", Name = $"{nameof(UsersController)}_{nameof(GetById)}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ApiResponse))]
    [ProducesResponseType(StatusCodes.Status503ServiceUnavailable, Type = typeof(ApiResponse))]
    public async Task<ActionResult<User>> GetById([FromRoute] string id)
    {
        User? userAccount = await userAccountQueryRepository.LoadAsync(id);

        return userAccount is null
            ? NotFound()
            : Ok(userAccount);
    }

    [HttpGet("me", Name = $"{nameof(UsersController)}_{nameof(GetMe)}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ApiResponse))]
    [ProducesResponseType(StatusCodes.Status503ServiceUnavailable, Type = typeof(ApiResponse))]
    public async Task<ActionResult<User>> GetMe()
    {
        User? userAccount = await userAccountQueryRepository.LoadAsync(appState.User.UserId!);

        if (userAccount is not null)
        {
            return Ok(userAccount);
        }

        var command = new CreateUserAccountCommand(
            appState.User.UserId,
            appState.User.Email,
            appState.User.Name,
            appState.User.Username
        );

        ICommandResult<User> result = await mediator.Send(command);

        return !result.IsSucceeded
            ? CommandFailure(result)
            : Ok(result.Data);
    }
}
