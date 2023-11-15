using System.Security.Claims;
using Nexus.Infra.Crosscutting.Extensions;

namespace Nexus.Infra.Crosscutting;

public sealed class AppUser
{
    public AppUser(ClaimsPrincipal principal)
    {
        Email = principal.GetEmail();
        UserName = principal.GetUserName();
        GivenName = principal.GetGivenName();
        UserId = principal.GetUserId();
        ClientId = principal.GetClientId();
        Roles = principal.GetRoles();
    }

    public string UserId { get; }
    public string Email { get; }
    public string UserName { get; }
    public string GivenName { get; }
    public string ClientId { get; }
    public IEnumerable<string> Roles { get; }
}
