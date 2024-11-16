using Goal.Domain.Events;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Nexus.Core.Domain.Customers.Aggregates;
using Nexus.Core.Domain.People.Aggregates;
using Nexus.Core.Domain.Users.Aggregates;
using Nexus.Core.Infra.Data.EventStore;
using Nexus.Core.Infra.Data.Repositories;

namespace Nexus.Core.Infra.Data.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCoreData(this IServiceCollection services, Action<CoreDataOptions> action)
    {
        var options = new CoreDataOptions();

        action?.Invoke(options);

        services.AddCoreDbContext(options.DefaultConnectionString);
        services.AddEventSourcingDbContext(options.EventSourcingConnectionString);
        services.AddEventStore();
        services.AddRepositories();
        services.AddUnitOfWork();

        return services;
    }

    public static IServiceCollection AddEventStore(this IServiceCollection services)
    {
        services.AddScoped<IEventStore, SqlEventStore>();

        return services;
    }

    public static IServiceCollection AddUnitOfWork(this IServiceCollection services)
    {
        services.AddScoped<ICoreUnitOfWork, CoreUnitOfWork>();

        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IPersonRepository, PersonRepository>();
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }

    private static IServiceCollection AddCoreDbContext(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<CoreDbContext>((provider, options) =>
        {
            options
                .UseSqlServer(connectionString, x => x.MigrationsAssembly(typeof(CoreDbContext).Assembly.GetName().Name))
                .EnableSensitiveDataLogging();
        });

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
