using Keycloak.AuthServices.Authentication;
using Keycloak.AuthServices.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nexus.Core.Application.Customers.Commands;
using Nexus.Core.Application.DependencyInjection;
using Nexus.Core.Domain.DependencyInjection;
using Nexus.Core.Infra.Data.DependencyInjection;
using Nexus.Core.Infra.Data.Query.DependencyInjection;
using Nexus.Infra.Crosscutting;
using Nexus.Infra.Http.Handlers;
using Raven.DependencyInjection;

namespace Nexus.Core.Infra.IoC.Extensions;

public static class ServiceColletionExtensionMethods
{
    public static IServiceCollection ConfigureApiServices(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
    {
        services.AddHttpContextAccessor();
        services.AddScoped(provider => provider.GetRequiredService<IHttpContextAccessor>().HttpContext!.User);
        services.AddScoped<AppState>();
        services.AddExceptionHandlers();

        services.AddCoreApplication(options =>
        {
            options.RegisterMediatRFromAssemblies(typeof(CustomerCommand).Assembly);
        });

        services.AddCoreData(options =>
        {
            options.UseDefaultConnectionString(configuration.GetConnectionString("DefaultConnection")!);
            options.UseEventSourcingConnectionString(configuration.GetConnectionString("EventSourcingConnection")!);
        });

        services.AddCoreDataQuery(settings =>
        {
            string urls = configuration["RavenSettings:Urls"] ?? string.Empty;
            settings = new RavenSettings
            {
                Urls = urls.Split(',', StringSplitOptions.RemoveEmptyEntries),
                DatabaseName = configuration["RavenSettings:DatabaseName"] ?? string.Empty,
                CertFilePath = configuration["RavenSettings:CertFilePath"],
                CertPassword = configuration["RavenSettings:CertPassword"],
            };

        });

        services.AddCoreDomain(opts =>
        {
            opts.UiAvatar.BaseAddress = configuration["UiAvatars:BaseAddress"] ?? string.Empty;
            opts.UiAvatar.DefaultBackground = configuration["UiAvatars:DefaultBackground"] ?? string.Empty;
            opts.UiAvatar.DefaultColor = configuration["UiAvatars:DefaultColor"] ?? string.Empty;
        });

        return services;
    }

    public static IServiceCollection ConfigureWorkerServices(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
    {
        services.AddCoreApplication();
        services.AddCoreData(options =>
        {
            options.UseDefaultConnectionString(configuration.GetConnectionString("DefaultConnection")!);
            options.UseEventSourcingConnectionString(configuration.GetConnectionString("EventSourcingConnection")!);
        });

        services.AddCoreDataQuery(settings =>
        {
            string urls = configuration["RavenSettings:Urls"] ?? string.Empty;
            settings = new RavenSettings
            {
                Urls = urls.Split(',', StringSplitOptions.RemoveEmptyEntries),
                DatabaseName = configuration["RavenSettings:DatabaseName"] ?? string.Empty,
                CertFilePath = configuration["RavenSettings:CertFilePath"],
                CertPassword = configuration["RavenSettings:CertPassword"],
            };

        });

        services.AddCoreDomain(opts =>
        {
            opts.UiAvatar.BaseAddress = configuration["UiAvatars:BaseAddress"] ?? string.Empty;
            opts.UiAvatar.DefaultBackground = configuration["UiAvatars:DefaultBackground"] ?? string.Empty;
            opts.UiAvatar.DefaultColor = configuration["UiAvatars:DefaultColor"] ?? string.Empty;
        });

        return services;
    }

    public static IServiceCollection AddExceptionHandlers(this IServiceCollection services)
    {
        services.AddExceptionHandler<NexusExceptionHandler>();
        services.AddProblemDetails();

        return services;
    }

    public static IServiceCollection AddKeycloak(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddKeycloakWebApi(configuration);

        services
            .AddAuthorization()
            .AddKeycloakAuthorization(options =>
            {
                options.EnableRolesMapping = RolesClaimTransformationSource.All;
                options.RolesResource = configuration["Keycloak:Resource"];
            })
            .AddAuthorizationBuilder()
            .AddPolicy("admin", policy =>
            {
                policy
                    .RequireAuthenticatedUser()
                    .RequireRole("admin");
            });

        return services;
    }
}
