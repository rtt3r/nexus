using System.Security.Claims;
using Nexus.Infra.Crosscutting.Extensions;

namespace Nexus.Infra.Crosscutting;

public sealed class AppState
{
    public AppState(ClaimsPrincipal principal)
    {
        User = new AppUser(principal);
        Session = new UserSession(principal);
        Scopes = principal.GetScopes();
    }

    public AppUser User { get; }
    public UserSession Session { get; }
    public IEnumerable<string> Scopes { get; }
}
