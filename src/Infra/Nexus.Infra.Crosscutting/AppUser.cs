using System.Security.Claims;
using Nexus.Infra.Crosscutting.Extensions;

namespace Nexus.Infra.Crosscutting;

public sealed class AppUser(ClaimsPrincipal principal)
{
    public string UserId { get; } = principal.GetUserId();
    public string Email { get; } = principal.GetEmail();
    public string Username { get; } = principal.GetUsername();
    public string Name { get; } = principal.GetName();
    public string GivenName { get; } = principal.GetGivenName();
    public string FamilyName { get; } = principal.GetFamilyName();
    public string ClientId { get; } = principal.GetClientId();
    public IEnumerable<string> Roles { get; } = principal.GetRoles();
}
