using System.Security.Claims;
using IdentityModel;
using Nexus.Infra.Crosscutting.Extensions;

namespace Nexus.Infra.Crosscutting;

public sealed class AppState(ClaimsPrincipal principal)
{
    public AppUser? User { get; } = principal.HasClaimValue(JwtClaimTypes.Subject, ClaimTypes.NameIdentifier)
        ? new AppUser(principal)
        : null;

    public AppClient Client { get; } = new AppClient(principal);
    public UserSession Session { get; } = new UserSession(principal);
    public IEnumerable<string> Scopes { get; } = principal.GetScopes();
}
