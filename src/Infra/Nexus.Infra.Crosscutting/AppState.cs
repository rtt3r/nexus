using System.Security.Claims;
using Nexus.Infra.Crosscutting.Extensions;

namespace Nexus.Infra.Crosscutting;

public sealed class AppState(ClaimsPrincipal principal)
{
    public AppUser User { get; } = new AppUser(principal);
    public UserSession Session { get; } = new UserSession(principal);
    public IEnumerable<string> Scopes { get; } = principal.GetScopes();
}
