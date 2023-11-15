using Nexus.Infra.Crosscutting.Settings;
using Nexus.Infra.Http.Swagger;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Nexus.Core.Api.Swagger;

public class ConfigureApiSwaggerOptions : ConfigureSwaggerOptions
{
    protected readonly IConfiguration configuration;

    public ConfigureApiSwaggerOptions(
        IConfiguration configuration)
        : base()
    {
        this.configuration = configuration;
    }

    public override void Configure(SwaggerGenOptions options)
    {
        base.Configure(options);

        KeycloakSettings keycloakOptions = configuration
            .GetSection(KeycloakSettings.Section)
            .Get<KeycloakSettings>();

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
                    TokenUrl = new Uri($"{keycloakOptions.AuthenticationOptions.KeycloakUrlRealm}/protocol/openid-connect/token"),
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
