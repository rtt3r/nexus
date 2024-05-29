using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nexus.Core.Infra.Data.EventSourcing;
using Nexus.Infra.Crosscutting.Providers.Data;

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