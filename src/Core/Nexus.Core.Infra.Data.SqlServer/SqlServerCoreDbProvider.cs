using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nexus.Infra.Crosscutting.Providers.Data;

namespace Nexus.Core.Infra.Data.SqlServer;

public class SqlServerCoreDbProvider : IDbProvider
{
    public IServiceCollection Configure(IServiceCollection services, IConfiguration configuration)
    {
        string? connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<CoreDbContext, SqlServerCoreDbContext>((provider, options) =>
        {
            options
                .UseSqlServer(connectionString, x => x.MigrationsAssembly(typeof(SqlServerCoreDbContext).Assembly.GetName().Name))
                .EnableSensitiveDataLogging();
        });

        return services;
    }
}