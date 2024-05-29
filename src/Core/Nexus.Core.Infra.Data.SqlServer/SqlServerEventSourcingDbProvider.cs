using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nexus.Core.Infra.Data.EventSourcing;
using Nexus.Infra.Crosscutting.Providers.Data;

namespace Nexus.Core.Infra.Data.SqlServer;

public class SqlServerEventSourcingDbProvider : IDbProvider
{
    public IServiceCollection Configure(IServiceCollection services, IConfiguration configuration)
    {
        string? connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<EventSourcingDbContext, SqlServerEventSourcingDbContext>((provider, options) =>
        {
            options
                .UseSqlServer(connectionString, x => x.MigrationsAssembly(typeof(SqlServerEventSourcingDbContext).Assembly.GetName().Name))
                .EnableSensitiveDataLogging();
        });

        return services;
    }
}