using Goal.Infra.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Nexus.Core.Infra.Data.Npgsql;

public class NpgsqlEventSourcingDbContextFactory : DesignTimeDbContextFactory<NpgsqlEventSourcingDbContext>
{
    protected override NpgsqlEventSourcingDbContext CreateNewInstance(DbContextOptionsBuilder<NpgsqlEventSourcingDbContext> optionsBuilder)
    {
        optionsBuilder.UseNpgsql(Configuration.GetConnectionString("NpgsqlConnection"));
        return new NpgsqlEventSourcingDbContext(optionsBuilder.Options);
    }
}
