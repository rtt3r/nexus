using Goal.Domain.Events;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Nexus.Infra.Data.EventSourcing;
using Nexus.Infra.Data.EventSourcing.EventStore;

namespace Nexus.Infra.Data.EventSourcing.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddEventSourcingData(this IServiceCollection services, Action<EventSourcingDataOptions> action)
    {
        var options = new EventSourcingDataOptions();

        action?.Invoke(options);

        services.AddEventSourcingDbContext(options.ConnectionString);
        services.AddEventStore();

        return services;
    }

    public static IServiceCollection AddEventStore(this IServiceCollection services)
    {
        services.AddScoped<IEventStore, SqlEventStore>();

        return services;
    }

    private static IServiceCollection AddEventSourcingDbContext(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<EventSourcingDbContext>((provider, options) =>
        {
            options
                .UseSqlServer(connectionString, x => x.MigrationsAssembly(typeof(EventSourcingDbContext).Assembly.GetName().Name))
                .EnableSensitiveDataLogging();
        });

        return services;
    }
}
