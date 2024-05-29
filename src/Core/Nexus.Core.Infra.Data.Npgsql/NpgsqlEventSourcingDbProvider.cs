using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nexus.Infra.Crosscutting.Providers.Data;
using Nexus.Core.Infra.Data.EventSourcing;

namespace Nexus.Core.Infra.Data.Npgsql;

public class NpgsqlEventSourcingDbProvider : IDbProvider
{
    public IServiceCollection Configure(IServiceCollection services, IConfiguration configuration)
    {
        string? connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<EventSourcingDbContext, NpgsqlEventSourcingDbContext>((provider, options) =>
        {
            options
                .UseNpgsql(connectionString, x => x.MigrationsAssembly(typeof(NpgsqlEventSourcingDbContext).Assembly.GetName().Name))
                .EnableSensitiveDataLogging();
        });

        return services;
    }
}