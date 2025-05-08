using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Nexus.Hcm.Infra.Data.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddHcmData(this IServiceCollection services, Action<HcmDataOptions> action)
    {
        var options = new HcmDataOptions();

        action?.Invoke(options);

        services.AddHcmDbContext(options.ConnectionString);

        services.AddRepositories();
        services.AddUnitOfWork();

        return services;
    }

    public static IServiceCollection AddUnitOfWork(this IServiceCollection services)
    {
        services.AddScoped<IHcmUnitOfWork, HcmUnitOfWork>();

        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services) =>
        // services.AddScoped<ILegalEntityRepository, LegalEntityRepository>();

        services;

    private static IServiceCollection AddHcmDbContext(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<HcmDbContext>((provider, options) =>
        {
            options
                .UseSqlServer(connectionString, x => x.MigrationsAssembly(typeof(HcmDbContext).Assembly.GetName().Name))
                .EnableSensitiveDataLogging();
        });

        return services;
    }
}
