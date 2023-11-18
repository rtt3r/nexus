using Nexus.Infra.Http.Controllers;
using Goal.Seedwork.Application.Commands;
using Goal.Seedwork.Infra.Http.Controllers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nexus.Infra.Crosscutting;
using Nexus.Core.Infra.Data.Query.Repositories.Users;
using Nexus.Core.Model.Users;
using Nexus.Core.Application.Commands.Users;

namespace Nexus.Core.Api.Controllers.Users;

[ApiController]
[ApiVersion("1")]
[Authorize]
[Route("v{version:apiVersion}/users/current")]
public class CurrentUserProfileController : ApiControllerBase
{
    private readonly IUserProfileQueryRepository userProfileQueryRepository;
    private readonly IMediator mediator;
    private readonly AppState appState;

    public CurrentUserProfileController(
        IUserProfileQueryRepository userProfileQueryRepository,
        IMediator mediator,
        AppState appState)
    {
        this.userProfileQueryRepository = userProfileQueryRepository;
        this.mediator = mediator;
        this.appState = appState;
    }

    [HttpGet("profile")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ApiResponse))]
    [ProducesResponseType(StatusCodes.Status503ServiceUnavailable, Type = typeof(ApiResponse))]
    public async Task<ActionResult<UserProfile>> Get()
    {
        var user = await userProfileQueryRepository.LoadAsync(appState.User.UserId);

        if (user is not null)
            return Ok(user);

        var command = new CreateUserProfileCommand
        {
            Id = appState.User.UserId
        };

        ICommandResult<UserProfile> result = await mediator.Send(command);

        return !result.IsSucceeded
            ? CommandFailure(result)
            : Ok(result.Data);
    }
}
