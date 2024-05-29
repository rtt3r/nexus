using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nexus.Infra.Crosscutting.Providers.Data;

namespace Nexus.Core.Infra.Data.Npgsql;

public class NpgsqlCoreDbProvider : IDbProvider
{
    public IServiceCollection Configure(IServiceCollection services, IConfiguration configuration)
    {
        string? connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<CoreDbContext, NpgsqlCoreDbContext>((provider, options) =>
        {
            options
                .UseNpgsql(connectionString, x => x.MigrationsAssembly(typeof(NpgsqlCoreDbContext).Assembly.GetName().Name))
                .EnableSensitiveDataLogging();
        });

        return services;
    }
}