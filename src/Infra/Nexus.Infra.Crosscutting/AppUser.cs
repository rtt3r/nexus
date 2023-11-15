using System.Security.Claims;
using Nexus.Infra.Crosscutting.Extensions;

namespace Nexus.Infra.Crosscutting;

public sealed class AppUser
{
    public AppUser(ClaimsPrincipal principal)
    {
        Email = principal.GetEmail();
        Username = principal.GetUsername();
        Name = principal.GetName();
        GivenName = principal.GetGivenName();
        FamilyName = principal.GetFamilyName();
        UserId = principal.GetUserId();
        ClientId = principal.GetClientId();
        Roles = principal.GetRoles();
    }

    public string UserId { get; }
    public string Email { get; }
    public string Username { get; }
    public string Name { get; }
    public string GivenName { get; }
    public string FamilyName { get; }
    public string ClientId { get; }
    public IEnumerable<string> Roles { get; }
}
