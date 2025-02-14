using Keycloak.AuthServices.Authentication;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;

namespace Nexus.Finance.Api.Infra.OpenApi;

internal sealed class BearerSecuritySchemeTransformer : IOpenApiDocumentTransformer
{
    private readonly IAuthenticationSchemeProvider _authenticationSchemeProvider;
    private readonly IConfiguration _configuration;

    public BearerSecuritySchemeTransformer(
        IAuthenticationSchemeProvider authenticationSchemeProvider,
        IConfiguration configuration)
    {
        _authenticationSchemeProvider = authenticationSchemeProvider;
        _configuration = configuration;
    }

    public async Task TransformAsync(OpenApiDocument document, OpenApiDocumentTransformerContext context, CancellationToken cancellationToken)
    {
        if (!(await _authenticationSchemeProvider.GetAllSchemesAsync())
            .Any(authScheme => authScheme.Name == "Bearer"))
        {
            return;
        }

        var keycloakOptions = _configuration
            .GetSection(KeycloakAuthenticationOptions.Section)
            .Get<KeycloakAuthenticationOptions>();

        if (keycloakOptions?.KeycloakUrlRealm is null) return;

        document.Components ??= new OpenApiComponents();
        document.Components.SecuritySchemes ??= new Dictionary<string, OpenApiSecurityScheme>();

        document.Components.SecuritySchemes["Bearer"] = new OpenApiSecurityScheme
        {
            Type = SecuritySchemeType.OAuth2,
            Scheme = "Bearer",
            In = ParameterLocation.Header,
            Name = HeaderNames.Authorization,
            Flows = new OpenApiOAuthFlows
            {
                Password = new OpenApiOAuthFlow
                {
                    TokenUrl = new Uri($"{keycloakOptions.KeycloakUrlRealm}protocol/openid-connect/token"),
                    Scopes = new Dictionary<string, string>()
                }
            }
        };

        var securityRequirement = new OpenApiSecurityRequirement
        {
            [new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "Bearer",
                Name = "Bearer",
                In = ParameterLocation.Header
            }] = Array.Empty<string>()
        };

        foreach (var operation in document.Paths.Values.SelectMany(path => path.Operations.Values))
        {
            operation.Security.Add(securityRequirement);
        }
    }
}
