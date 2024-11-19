using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Nexus.Finance.Domain.Accounts.Aggregates;
using Nexus.Finance.Infra.Data.Repositories;

namespace Nexus.Finance.Infra.Data.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddFinanceData(this IServiceCollection services, Action<FinanceDataOptions> action)
    {
        var options = new FinanceDataOptions();

        action?.Invoke(options);

        services.AddFinanceDbContext(options.ConnectionString);

        services.AddRepositories();
        services.AddUnitOfWork();

        return services;
    }

    public static IServiceCollection AddUnitOfWork(this IServiceCollection services)
    {
        services.AddScoped<IFinanceUnitOfWork, FinanceUnitOfWork>();

        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IAccountRepository, AccountRepository>();
        services.AddScoped<IFinancialInstitutionRepository, FinancialInstitutionRepository>();

        return services;
    }

    private static IServiceCollection AddFinanceDbContext(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<FinanceDbContext>((provider, options) =>
        {
            options
                .UseSqlServer(connectionString, x => x.MigrationsAssembly(typeof(FinanceDbContext).Assembly.GetName().Name))
                .EnableSensitiveDataLogging();
        });

        return services;
    }
}
