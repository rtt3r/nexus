using System.Security.Claims;
using Nexus.Infra.Crosscutting.Extensions;

namespace Nexus.Infra.Crosscutting
{
    public class UserSession(ClaimsPrincipal principal)
    {
        public DateTime ExpiresAt { get; private set; } = new DateTime(principal.GetExpiration(), DateTimeKind.Unspecified);
        public DateTime IssuedAt { get; private set; } = new DateTime(principal.GetIssuedAt(), DateTimeKind.Unspecified);
    }
}