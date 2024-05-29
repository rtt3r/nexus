using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nexus.Infra.Crosscutting.Providers.Data;
using Nexus.Core.Infra.Data.EventSourcing;

namespace Nexus.Core.Infra.Data.MySql;

public class MySqlEventSourcingDbProvider : IDbProvider
{
    public IServiceCollection Configure(IServiceCollection services, IConfiguration configuration)
    {
        string? connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<EventSourcingDbContext, MySqlEventSourcingDbContext>((provider, options) =>
        {
            options
                .UseMySql(
                    connectionString,
                    new MySqlServerVersion(new Version(8, 2, 0)),
                    x => x.MigrationsAssembly(typeof(MySqlEventSourcingDbContext).Assembly.GetName().Name)
                )
                .EnableSensitiveDataLogging();
        });

        return services;
    }
}