using System.Diagnostics;
using Goal.Domain.Aggregates;
using Goal.Domain.Events;
using Goal.Infra.Data.Query;
using Goal.Infra.Http.DependencyInjection;
using Keycloak.AuthServices.Authentication;
using Keycloak.AuthServices.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nexus.Core.Application.TypeAdapters;
using Nexus.Core.Domain.Users.Services;
using Nexus.Core.Infra.Data;
using Nexus.Core.Infra.Data.EventSourcing;
using Nexus.Core.Infra.Data.MySql;
using Nexus.Core.Infra.Data.Npgsql;
using Nexus.Core.Infra.Data.Query.Repositories.Customers;
using Nexus.Core.Infra.Data.Repositories;
using Nexus.Core.Infra.Data.SqlServer;
using Nexus.Infra.Crosscutting;
using Nexus.Infra.Crosscutting.Settings;
using Raven.DependencyInjection;
using Serilog;

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

        services.AddDefaultNotificationHandler();
        services.AddRavenDb(configuration);

        services.AddCoreDbContext(configuration);
        services.AddScoped<ICoreUnitOfWork, CoreUnitOfWork>();
        services.AddScoped<IGenerateUserProfileAvatarDomainService, GenerateUserProfileAvatarDomainService>();
        services.RegisterAllTypesOf<IRepository>(typeof(CustomerRepository).Assembly);
        services.RegisterAllTypesOf<IQueryRepository>(typeof(CustomerQueryRepository).Assembly);

        return services;
    }

    public static IServiceCollection ConfigureWorkerServices(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
    {
        services.AddAutoMapperTypeAdapter();

        services.AddDefaultNotificationHandler();
        services.AddRavenDb(configuration);

        services.AddEventSourcingDbContext(configuration);
        services.AddScoped<IEventStore, SqlEventStore>();

        services.RegisterAllTypesOf<IQueryRepository>(typeof(CustomerQueryRepository).Assembly);

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
                options.EnableRolesMapping =
                    RolesClaimTransformationSource.ResourceAccess;
                options.RolesResource = configuration["Keycloak:Resource"];
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
        string? dbProvider = configuration.GetValue("DbProvider", "SqlServer");
        string? connectionString = configuration.GetConnectionString("DefaultConnection");

        Log.Information("DbProvider = {dbProvider}", dbProvider);
        Log.Information("ConnectionString = {connectionString}", connectionString);

        if (dbProvider == "SqlServer")
        {
            services.AddDbContext<CoreDbContext, SqlServerCoreDbContext>((provider, options) =>
            {
                options
                    .UseSqlServer(connectionString, x => x.MigrationsAssembly(typeof(SqlServerCoreDbContext).Assembly.GetName().Name))
                    .EnableSensitiveDataLogging();
            });
        }
        else if (dbProvider == "MySql")
        {
            services.AddDbContext<CoreDbContext, MySqlCoreDbContext>((provider, options) =>
            {
                options
                    .UseMySql(
                        connectionString,
                        new MySqlServerVersion(new Version(8, 2, 0)),
                        x => x.MigrationsAssembly(typeof(MySqlCoreDbContext).Assembly.GetName().Name)
                    )
                    .EnableSensitiveDataLogging();
            });
        }
        else if (dbProvider == "Npgsql")
        {
            services.AddDbContext<CoreDbContext, NpgsqlCoreDbContext>((provider, options) =>
            {
                options
                    .UseNpgsql(connectionString, x => x.MigrationsAssembly(typeof(NpgsqlCoreDbContext).Assembly.GetName().Name))
                    .EnableSensitiveDataLogging();
            });
        }
        else
        {
            throw new InvalidOperationException($"Unsupported provider: {dbProvider}");
        }

        return services;
    }

    private static IServiceCollection AddEventSourcingDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        string? dbProvider = configuration.GetValue("DbProvider", "SqlServer");
        string? connectionString = configuration.GetConnectionString("DefaultConnection");

        if (dbProvider == "SqlServer")
        {
            services.AddDbContext<EventSourcingDbContext, SqlServerEventSourcingDbContext>((provider, options) =>
            {
                options
                    .UseSqlServer(connectionString, x => x.MigrationsAssembly(typeof(SqlServerEventSourcingDbContext).Assembly.GetName().Name))
                    .EnableSensitiveDataLogging();
            });
        }
        else if (dbProvider == "MySql")
        {
            services.AddDbContext<EventSourcingDbContext, MySqlEventSourcingDbContext>((provider, options) =>
            {
                options
                    .UseMySql(
                        connectionString,
                        new MySqlServerVersion(new Version(8, 2, 0)),
                        x => x.MigrationsAssembly(typeof(MySqlEventSourcingDbContext).Assembly.GetName().Name)
                    )
                    .EnableSensitiveDataLogging();
            });
        }
        else if (dbProvider == "Npgsql")
        {
            services.AddDbContext<EventSourcingDbContext, NpgsqlEventSourcingDbContext>((provider, options) =>
            {
                options
                    .UseNpgsql(connectionString, x => x.MigrationsAssembly(typeof(NpgsqlEventSourcingDbContext).Assembly.GetName().Name))
                    .EnableSensitiveDataLogging();
            });
        }
        else
        {
            throw new Exception($"Unsupported provider: {dbProvider}");
        }

        return services;
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
