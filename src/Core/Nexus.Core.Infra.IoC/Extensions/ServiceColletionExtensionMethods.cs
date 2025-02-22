using Keycloak.AuthServices.Authentication;
using Keycloak.AuthServices.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nexus.Core.Application.Persons.Commands;
using Nexus.Core.Application.Extensions.DependencyInjection;
using Nexus.Core.Domain.Extensions.DependencyInjection;
using Nexus.Core.Infra.Data.Extensions.DependencyInjection;
using Nexus.Core.Infra.Data.Query.DependencyInjection;
using Nexus.Infra.Crosscutting;
using Nexus.Infra.Data.EventSourcing.Extensions.DependencyInjection;
using Nexus.Infra.Http.Handlers;

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
            options.RegisterMediatRFromAssemblies(typeof(PersonCommand).Assembly);
        });

        services.AddCoreData(options =>
        {
            options.UseConnectionString(configuration.GetConnectionString("DefaultConnection")!);
        });

        services.AddCoreDataQuery(settings =>
        {
            settings.Urls = (configuration["RavenSettings:Urls"] ?? string.Empty).Split(',', StringSplitOptions.RemoveEmptyEntries);
            settings.DatabaseName = configuration["RavenSettings:DatabaseName"] ?? string.Empty;
            settings.CertFilePath = configuration["RavenSettings:CertFilePath"];
            settings.CertPassword = configuration["RavenSettings:CertPassword"];
        });

        services.AddCoreDomain();

        return services;
    }

    public static IServiceCollection ConfigureWorkerServices(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
    {
        services.AddCoreApplication();

        services.AddCoreData(options =>
        {
            options.UseConnectionString(configuration.GetConnectionString("DefaultConnection")!);
        });

        services.AddEventSourcingData(options =>
        {
            options.UseConnectionString(configuration.GetConnectionString("EventSourcingConnection")!);
        });

        services.AddCoreDataQuery(settings =>
        {
            settings.Urls = (configuration["RavenSettings:Urls"] ?? string.Empty).Split(',', StringSplitOptions.RemoveEmptyEntries);
            settings.DatabaseName = configuration["RavenSettings:DatabaseName"] ?? string.Empty;
            settings.CertFilePath = configuration["RavenSettings:CertFilePath"];
            settings.CertPassword = configuration["RavenSettings:CertPassword"];
        });

        services.AddCoreDomain();

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
