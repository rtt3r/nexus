using System.Security.Claims;
using System.Security.Principal;
using IdentityModel;

namespace Nexus.Infra.Crosscutting.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static string? GetClaimValue(this ClaimsPrincipal principal, params string[] claimTypes)
        => principal?.GetClaimValues(claimTypes).FirstOrDefault();

    public static IEnumerable<string> GetClaimValues(this ClaimsPrincipal principal, params string[] claimTypes)
    {
        var claimValues = principal
            .Claims
            .Where(p => claimTypes.Contains(p.Type))
            .Select(p => p.Value)
            .ToList();

        return claimValues;
    }

    public static Guid GetClaimValueAsGuid(this ClaimsPrincipal principal, params string[] claimTypes)
    {
        string? claimValue = principal.GetClaimValue(claimTypes);

        if (string.IsNullOrWhiteSpace(claimValue))
        {
            return Guid.Empty;
        }

        return Guid.TryParse(claimValue, out Guid value)
            ? value
            : Guid.Empty;
    }

    public static T GetClaimValueAs<T>(this ClaimsPrincipal principal, params string[] claimTypes)
        where T : struct
    {
        if (principal is null)
        {
            return default;
        }

        string? claimValue = principal.GetClaimValue(claimTypes);

        if (string.IsNullOrWhiteSpace(claimValue))
        {
            return default;
        }

        try
        {
            return (T)Convert.ChangeType(claimValue, typeof(T));
        }
        catch
        {
            return default;
        }
    }

    public static string GetUserId(this ClaimsPrincipal principal)
        => principal.GetClaimValue(JwtClaimTypes.Subject, ClaimTypes.NameIdentifier)!;

    public static string GetEmail(this ClaimsPrincipal principal)
        => principal?.GetClaimValue(JwtClaimTypes.Email, ClaimTypes.Email)!;

    public static string GetName(this ClaimsPrincipal principal)
        => principal?.GetClaimValue(JwtClaimTypes.Name, ClaimTypes.Name)!;

    public static string GetUsername(this ClaimsPrincipal principal)
        => principal?.GetClaimValue(JwtClaimTypes.PreferredUserName)!;

    public static string? GetGivenName(this ClaimsPrincipal principal)
        => principal?.GetClaimValue(JwtClaimTypes.GivenName, ClaimTypes.GivenName);

    public static string? GetFamilyName(this ClaimsPrincipal principal)
        => principal?.GetClaimValue(JwtClaimTypes.FamilyName);

    public static IEnumerable<string> GetRoles(this ClaimsPrincipal principal)
        => principal?.GetClaimValues(JwtClaimTypes.Role, ClaimTypes.Role) ?? [];

    public static IEnumerable<string> GetScopes(this ClaimsPrincipal principal)
        => principal?.GetClaimValues(JwtClaimTypes.Scope) ?? [];

    public static string GetClientId(this ClaimsPrincipal principal)
        => principal?.GetClaimValue(JwtClaimTypes.ClientId, JwtClaimTypes.AuthorizedParty)!;

    public static long GetExpiration(this ClaimsPrincipal principal)
        => (principal?.GetClaimValueAs<long>(JwtClaimTypes.Expiration, ClaimTypes.Expiration)).GetValueOrDefault();

    public static long GetIssuedAt(this ClaimsPrincipal principal)
        => (principal?.GetClaimValueAs<long>(JwtClaimTypes.IssuedAt)).GetValueOrDefault();

    public static void AddClaim(this IIdentity identity, Claim claim)
    {
        if (identity is ClaimsIdentity claimsIdentity)
        {
            claimsIdentity.AddClaim(claim);
        }
    }

    public static bool HasClaimValue(this ClaimsPrincipal principal, params string[] claimTypes)
        => !string.IsNullOrWhiteSpace(principal.GetClaimValue(claimTypes));
}
