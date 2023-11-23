using System.Security.Claims;
using Nexus.Infra.Crosscutting.Extensions;

namespace Nexus.Infra.Crosscutting
{
    public class UserSession
    {
        public UserSession(ClaimsPrincipal principal)
        {
            ExpiresAt = new DateTime(principal.GetExpiration(), DateTimeKind.Unspecified);
            IssuedAt = new DateTime(principal.GetIssuedAt(), DateTimeKind.Unspecified);
        }

        public DateTime ExpiresAt { get; private set; }
        public DateTime IssuedAt { get; private set; }
    }
}