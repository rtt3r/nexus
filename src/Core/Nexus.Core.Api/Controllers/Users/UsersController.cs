using Nexus.Core.Application.Commands.Users;
using Nexus.Core.Infra.Data.Query.Repositories.Users;
using Nexus.Core.Model.Users;
using Nexus.Infra.Http.Controllers;
using Goal.Seedwork.Application.Commands;
using Goal.Seedwork.Infra.Http.Controllers;
using Goal.Seedwork.Infra.Http.Controllers.Requests;
using Goal.Seedwork.Infra.Http.Controllers.Results;
using Goal.Seedwork.Infra.Http.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nexus.Infra.Crosscutting;

namespace Nexus.Core.Api.Controllers.Users;

[ApiController]
[ApiVersion("1")]
[Authorize]
[Route("v{version:apiVersion}/[controller]")]
public class UsersController : ApiControllerBase
{
    private readonly IUserQueryRepository userQueryRepository;
    private readonly IMediator mediator;
    private readonly AppState appState;

    public UsersController(
        IUserQueryRepository UserQueryRepository,
        IMediator mediator,
        AppState appState)
    {
        this.userQueryRepository = UserQueryRepository;
        this.mediator = mediator;
        this.appState = appState;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ApiResponse))]
    public async Task<ActionResult<PagedResponse<User>>> Get([FromQuery] PageSearchRequest request)
        => Paged(await userQueryRepository.QueryAsync(request.ToPageSearch()));

    [HttpGet("current")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ApiResponse))]
    [ProducesResponseType(StatusCodes.Status503ServiceUnavailable, Type = typeof(ApiResponse))]
    public async Task<ActionResult<User>> GetCurrent()
    {
        User user = await userQueryRepository.LoadAsync(appState.User.UserId);

        if (user is not null)
            return Ok(user);

        var command = new RegisterUserCommand(
            appState.User.UserId,
            appState.User.Email,
            appState.User.Name);

        ICommandResult<User> result = await mediator
            .Send<ICommandResult<User>>(command);

        return !result.IsSucceeded
            ? CommandFailure(result)
            : Ok(result.Data);
    }

    [HttpGet("{id}", Name = $"{nameof(UsersController)}_{nameof(GetById)}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ApiResponse))]
    [ProducesResponseType(StatusCodes.Status503ServiceUnavailable, Type = typeof(ApiResponse))]
    public async Task<ActionResult<User>> GetById([FromRoute] string id)
    {
        User User = await userQueryRepository.LoadAsync(id);

        return User is null
            ? NotFound()
            : Ok(User);
    }
}
