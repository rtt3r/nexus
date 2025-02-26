using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Nexus.Core.Domain.Persons.Aggregates;
using Nexus.Core.Infra.Data.Repositories;

namespace Nexus.Core.Infra.Data.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCoreData(this IServiceCollection services, Action<CoreDataOptions> action)
    {
        var options = new CoreDataOptions();

        action?.Invoke(options);

        services.AddCoreDbContext(options.ConnectionString);

        services.AddRepositories();
        services.AddUnitOfWork();

        return services;
    }

    public static IServiceCollection AddUnitOfWork(this IServiceCollection services)
    {
        services.AddScoped<ICoreUnitOfWork, CoreUnitOfWork>();

        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IDocumentTypeRepository, DocumentTypeRepository>();
        services.AddScoped<ILegalEntityRepository, LegalEntityRepository>();
        services.AddScoped<INaturalPersonRepository, NaturalPersonRepository>();
        services.AddScoped<IPersonRepository, PersonRepository>();

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
}
