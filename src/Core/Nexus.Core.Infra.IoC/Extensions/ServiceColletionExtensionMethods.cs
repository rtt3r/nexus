using Goal.Domain.Aggregates;
using Goal.Domain.Events;
using Goal.Infra.Data.Query;
using Goal.Infra.Http.DependencyInjection;
using Keycloak.AuthServices.Authentication;
using Keycloak.AuthServices.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nexus.Core.Application.TypeAdapters;
using Nexus.Core.Domain.Users.Services;
using Nexus.Core.Infra.Data;
using Nexus.Core.Infra.Data.EventSourcing;
using Nexus.Core.Infra.Data.Query.Repositories.Customers;
using Nexus.Core.Infra.Data.Repositories;
using Nexus.Core.Infra.IoC.Providers;
using Nexus.Infra.Crosscutting;
using Nexus.Infra.Crosscutting.Exceptions;
using Nexus.Infra.Crosscutting.Providers.Data;
using Nexus.Infra.Crosscutting.Settings;
using Nexus.Infra.Http.Handlers.Exceptions;
using Raven.DependencyInjection;

namespace Nexus.Core.Infra.IoC.Extensions;

public static class ServiceColletionExtensionMethods
{
    public static IServiceCollection ConfigureApiServices(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
    {
        services.AddHttpContextAccessor();
        services.AddScoped(provider => provider.GetRequiredService<IHttpContextAccessor>().HttpContext!.User);
        services.AddScoped<AppState>();

        services.Configure<UiAvatarsOptions>(configuration.GetSection("UiAvatars"));

        services.AddAutoMapperTypeAdapter();
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));

        services.AddExceptionHandlers();
        services.AddRavenDb(configuration);

        services.AddCoreDbContext(configuration);
        services.AddScoped<ICoreUnitOfWork, CoreUnitOfWork>();
        services.AddScoped<IGenerateUserAvatarDomainService, GenerateUserAvatarDomainService>();
        services.RegisterAllTypesOf<IRepository>(typeof(CustomerRepository).Assembly);
        services.RegisterAllTypesOf<IQueryRepository>(typeof(CustomerQueryRepository).Assembly);

        return services;
    }

    public static IServiceCollection ConfigureWorkerServices(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
    {
        services.AddAutoMapperTypeAdapter();

        services.AddRavenDb(configuration);

        services.AddEventSourcingDbContext(configuration);
        services.AddScoped<IEventStore, SqlEventStore>();

        services.RegisterAllTypesOf<IQueryRepository>(typeof(CustomerQueryRepository).Assembly);

        return services;
    }

    public static IServiceCollection AddExceptionHandlers(this IServiceCollection services)
    {
        services.AddExceptionHandler<RequestValidationExceptionHandler>();
        services.AddExceptionHandler<ResourceNotFoundExceptionHandler>();
        services.AddExceptionHandler<DomainViolationExceptionHandler>();
        services.AddExceptionHandler<ServiceUnavailableExceptionHandler>();
        services.AddExceptionHandler<InternalServerErrorExceptionHandler>();
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

    private static IServiceCollection AddAutoMapperTypeAdapter(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(AutoMapperAdapterFactory).Assembly);
        services.AddTypeAdapterFactory<AutoMapperAdapterFactory>();

        return services;
    }

    private static IServiceCollection AddCoreDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        try
        {
            DbProvider enDbProvider = configuration.GetValue<DbProvider>("DbProvider");
            IDbProvider dbProvider = DbProviderFactory.Core.CreateProvider(enDbProvider);

            return dbProvider.Configure(services, configuration);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Unsupported Db Provider: {configuration.GetValue<string>("DbProvider")}", ex);
        }
    }

    private static IServiceCollection AddEventSourcingDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        try
        {
            DbProvider enDbProvider = configuration.GetValue<DbProvider>("DbProvider");
            IDbProvider dbProvider = DbProviderFactory.EventSourcing.CreateProvider(enDbProvider);

            return dbProvider.Configure(services, configuration);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Unsupported Db Provider: {configuration.GetValue<string>("DbProvider")}", ex);
        }
    }

    private static IServiceCollection AddRavenDb(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<RavenSettings>(configuration.GetSection("RavenSettings"));

        services.AddRavenDbDocStore(opts =>
        {
            string urls = configuration["RavenSettings:Urls"] ?? string.Empty;
            opts.Settings = new RavenSettings
            {
                Urls = urls.Split(',', StringSplitOptions.RemoveEmptyEntries),
                DatabaseName = configuration["RavenSettings:DatabaseName"] ?? string.Empty,
                CertFilePath = configuration["RavenSettings:CertFilePath"],
                CertPassword = configuration["RavenSettings:CertPassword"],
            };
        });

        services.AddRavenDbAsyncSession();
        services.AddRavenDbSession();

        return services;
    }
}
