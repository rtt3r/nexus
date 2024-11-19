using Keycloak.AuthServices.Authentication;
using Keycloak.AuthServices.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nexus.Finance.Application.Accounts.Commands;
using Nexus.Finance.Application.Extensions.DependencyInjection;
using Nexus.Finance.Domain.Extensions.DependencyInjection;
using Nexus.Finance.Infra.Data.Extensions.DependencyInjection;
using Nexus.Finance.Infra.Data.Query.DependencyInjection;
using Nexus.Infra.Crosscutting;
using Nexus.Infra.Data.EventSourcing.Extensions.DependencyInjection;
using Nexus.Infra.Http.Handlers;

namespace Nexus.Finance.Infra.IoC.Extensions;

public static class ServiceColletionExtensionMethods
{
    public static IServiceCollection ConfigureApiServices(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
    {
        services.AddHttpContextAccessor();
        services.AddScoped(provider => provider.GetRequiredService<IHttpContextAccessor>().HttpContext!.User);
        services.AddScoped<AppState>();
        services.AddExceptionHandlers();

        services.AddFinanceApplication(options =>
        {
            options.RegisterMediatRFromAssemblies(typeof(AccountCommand).Assembly);
        });

        services.AddFinanceData(options =>
        {
            options.UseConnectionString(configuration.GetConnectionString("DefaultConnection")!);
        });

        services.AddFinanceDataQuery(settings =>
        {
            settings.Urls = (configuration["RavenSettings:Urls"] ?? string.Empty).Split(',', StringSplitOptions.RemoveEmptyEntries);
            settings.DatabaseName = configuration["RavenSettings:DatabaseName"] ?? string.Empty;
            settings.CertFilePath = configuration["RavenSettings:CertFilePath"];
            settings.CertPassword = configuration["RavenSettings:CertPassword"];
        });

        services.AddFinanceDomain(opts => { });

        return services;
    }

    public static IServiceCollection ConfigureWorkerServices(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
    {
        services.AddFinanceApplication();

        services.AddFinanceData(options =>
        {
            options.UseConnectionString(configuration.GetConnectionString("DefaultConnection")!);
        });

        services.AddEventSourcingData(options =>
        {
            options.UseConnectionString(configuration.GetConnectionString("EventSourcingConnection")!);
        });

        services.AddFinanceDataQuery(settings =>
        {
            settings.Urls = (configuration["RavenSettings:Urls"] ?? string.Empty).Split(',', StringSplitOptions.RemoveEmptyEntries);
            settings.DatabaseName = configuration["RavenSettings:DatabaseName"] ?? string.Empty;
            settings.CertFilePath = configuration["RavenSettings:CertFilePath"];
            settings.CertPassword = configuration["RavenSettings:CertPassword"];
        });

        services.AddFinanceDomain(opts => { });

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
