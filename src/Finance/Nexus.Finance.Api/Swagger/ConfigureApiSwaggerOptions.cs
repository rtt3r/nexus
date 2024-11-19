using Keycloak.AuthServices.Authentication;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using Nexus.Infra.Http.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Nexus.Finance.Api.Swagger;

public class ConfigureApiSwaggerOptions(
    IConfiguration configuration)
    : ConfigureSwaggerOptions()
{
    protected readonly IConfiguration configuration = configuration;

    public override void Configure(SwaggerGenOptions options)
    {
        base.Configure(options);

        KeycloakAuthenticationOptions? keycloakOptions = configuration
            .GetSection(KeycloakAuthenticationOptions.Section)
            .Get<KeycloakAuthenticationOptions>();

        options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
        {
            Type = SecuritySchemeType.OAuth2,
            Scheme = "Bearer",
            In = ParameterLocation.Header,
            Name = HeaderNames.Authorization,
            Flows = new OpenApiOAuthFlows
            {
                Password = new OpenApiOAuthFlow
                {
                    TokenUrl = new Uri($"{keycloakOptions?.KeycloakUrlRealm}protocol/openid-connect/token"),
                    Scopes = new Dictionary<string, string>()
                }
            }
        });

        options.AddSecurityRequirement(new OpenApiSecurityRequirement()
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "oauth2"
                    },
                    Scheme = "oauth2",
                    Name = "oauth2",
                    In = ParameterLocation.Header
                },
                Array.Empty<string>()
            }
        });
    }
}
