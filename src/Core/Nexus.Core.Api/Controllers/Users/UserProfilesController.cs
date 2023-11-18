using Nexus.Infra.Http.Controllers;
using Goal.Seedwork.Infra.Http.Controllers;
using Goal.Seedwork.Infra.Http.Controllers.Requests;
using Goal.Seedwork.Infra.Http.Controllers.Results;
using Goal.Seedwork.Infra.Http.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nexus.Infra.Crosscutting;
using Nexus.Core.Infra.Data.Query.Repositories.Users;
using Nexus.Core.Model.Users;

namespace Nexus.Core.Api.Controllers.Users;

[ApiController]
[ApiVersion("1")]
[Authorize]
[Route("v{version:apiVersion}/user-profiles")]
public class UserProfilesController : ApiControllerBase
{
    private readonly IUserProfileQueryRepository userProfileQueryRepository;

    public UserProfilesController(
        IUserProfileQueryRepository userProfileQueryRepository,
        IMediator mediator,
        AppState appState)
    {
        this.userProfileQueryRepository = userProfileQueryRepository;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ApiResponse))]
    public async Task<ActionResult<PagedResponse<UserProfile>>> Get([FromQuery] PageSearchRequest request)
        => Paged(await userProfileQueryRepository.QueryAsync(request.ToPageSearch()));
}
