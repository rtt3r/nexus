using Microsoft.Extensions.DependencyInjection;
using Nexus.Hcm.Infra.Data.Query.Repositories.People;
using Raven.DependencyInjection;

namespace Nexus.Hcm.Infra.Data.Query.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddHcmDataQuery(this IServiceCollection services, Action<RavenSettings> action)
    {
        var options = new RavenSettings();

        action?.Invoke(options);

        services.AddRavenDb(options);
        services.AddRepositories();

        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IEmployeeQueryRepository, EmployeeQueryRepository>();

        return services;
    }

    private static IServiceCollection AddRavenDb(this IServiceCollection services, RavenSettings settings)
    {
        services.AddRavenDbDocStore(opts =>
        {
            opts.Settings = settings;
        });

        services.AddRavenDbAsyncSession();
        services.AddRavenDbSession();

        return services;
    }
}
