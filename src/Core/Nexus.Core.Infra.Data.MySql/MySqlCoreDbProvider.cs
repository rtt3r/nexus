using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nexus.Infra.Crosscutting.Providers.Data;

namespace Nexus.Core.Infra.Data.MySql;

public class MySqlCoreDbProvider : IDbProvider
{
    public IServiceCollection Configure(IServiceCollection services, IConfiguration configuration)
    {
        string? connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<CoreDbContext, MySqlCoreDbContext>((provider, options) =>
        {
            options.UseMySql(
                connectionString,
                new MySqlServerVersion(new Version(8, 2, 0)),
                x => x.MigrationsAssembly(typeof(MySqlCoreDbContext).Assembly.GetName().Name)
            )
            .EnableSensitiveDataLogging();
        });

        return services;
    }
}