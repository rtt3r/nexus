using Asp.Versioning;
using Goal.Infra.Http.Controllers;
using Goal.Infra.Http.Controllers.Requests;
using Goal.Infra.Http.Controllers.Results;
using Goal.Infra.Http.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nexus.Core.Application.Commands.Users;
using Nexus.Core.Infra.Data.Query.Repositories.Users;
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
    IUserQueryRepository userQueryRepository)
    : ApiController
{
    private readonly AppState appState = appState;
    private readonly IMediator mediator = mediator;
    private readonly IUserQueryRepository userQueryRepository = userQueryRepository;

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ApiResponse))]
    public async Task<ActionResult<PagedResponse<User>>> Get([FromQuery] PageSearchRequest request)
        => Paged(await userQueryRepository.QueryAsync(request.ToPageSearch()));

    [HttpGet("{id}", Name = $"{nameof(UsersController)}_{nameof(GetById)}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ApiResponse))]
    [ProducesResponseType(StatusCodes.Status503ServiceUnavailable, Type = typeof(ApiResponse))]
    public async Task<ActionResult<User>> GetById([FromRoute] string id)
    {
        User? user = await userQueryRepository.LoadAsync(id);

        return user is null
            ? NotFound()
            : Ok(user);
    }

    [HttpGet("me", Name = $"{nameof(UsersController)}_{nameof(GetMe)}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ApiResponse))]
    [ProducesResponseType(StatusCodes.Status503ServiceUnavailable, Type = typeof(ApiResponse))]
    public async Task<ActionResult<User>> GetMe()
    {
        User? user = await userQueryRepository.LoadAsync(appState.User!.UserId);

        if (user is not null)
        {
            return Ok(user);
        }

        var command = new CreateUserCommand(
            appState.User.UserId,
            appState.User.Name,
            appState.User.Email,
            appState.User.Username
        );

        return Ok(await mediator.Send<User>(command));
    }
}
