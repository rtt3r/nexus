using Nexus.Infra.Http.Controllers;
using Goal.Seedwork.Application.Commands;
using Goal.Seedwork.Infra.Http.Controllers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nexus.Infra.Crosscutting;
using Nexus.Core.Model.Users;
using Nexus.Core.Application.Commands.Users;
using Nexus.Core.Infra.Data.Query.Repositories.Users.Profiles;
using Nexus.Core.Infra.Data.Query.Repositories.Users.Accounts;

namespace Nexus.Core.Api.Controllers.Users;

[ApiController]
[ApiVersion("1")]
[Authorize]
[Route("v{version:apiVersion}/users/current")]
public class CurrentUserController : ApiControllerBase
{
    private readonly IUserAccountQueryRepository userAccountQueryRepository;
    private readonly IMediator mediator;
    private readonly AppState appState;

    public CurrentUserController(
        IUserAccountQueryRepository userAccountQueryRepository,
        IMediator mediator,
        AppState appState)
    {
        this.userAccountQueryRepository = userAccountQueryRepository;
        this.mediator = mediator;
        this.appState = appState;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ApiResponse))]
    [ProducesResponseType(StatusCodes.Status503ServiceUnavailable, Type = typeof(ApiResponse))]
    public async Task<ActionResult<UserAccount>> Get()
    {
        var user = await userAccountQueryRepository.LoadAsync(appState.User.UserId);

        if (user is not null)
            return Ok(user);

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
}
