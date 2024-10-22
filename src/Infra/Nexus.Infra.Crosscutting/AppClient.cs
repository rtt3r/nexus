using System.Security.Claims;
using Nexus.Infra.Crosscutting.Extensions;

namespace Nexus.Infra.Crosscutting;

public sealed class AppClient(ClaimsPrincipal principal)
{
    public string ClientId { get; } = principal.GetClientId();
}