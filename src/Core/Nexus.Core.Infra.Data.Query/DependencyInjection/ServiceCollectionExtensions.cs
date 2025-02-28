using Microsoft.Extensions.DependencyInjection;
using Nexus.Core.Infra.Data.Query.Repositories.Accounts;
using Raven.DependencyInjection;

namespace Nexus.Core.Infra.Data.Query.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCoreDataQuery(this IServiceCollection services, Action<RavenSettings> action)
    {
        var options = new RavenSettings();

        action?.Invoke(options);

        services.AddRavenDb(options);
        services.AddRepositories();

        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IAccountQueryRepository, AccountQueryRepository>();

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
