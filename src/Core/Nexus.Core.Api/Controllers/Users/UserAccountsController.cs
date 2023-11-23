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

namespace Nexus.Core.Api.Controllers.Users;

[ApiController]
[ApiVersion("1")]
[Authorize]
[Route("v{version:apiVersion}/user-accounts")]
public class UserAccountsController : ApiControllerBase
{
    private readonly IUserAccountQueryRepository userAccountQueryRepository;

    public UserAccountsController(
        IUserAccountQueryRepository userAccountQueryRepository,
        IMediator mediator,
        AppState appState)
    {
        this.userAccountQueryRepository = userAccountQueryRepository;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ApiResponse))]
    public async Task<ActionResult<PagedResponse<UserAccount>>> Get([FromQuery] PageSearchRequest request)
        => Paged(await userAccountQueryRepository.QueryAsync(request.ToPageSearch()));
}
