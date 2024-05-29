using Goal.Infra.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Nexus.Core.Infra.Data.Npgsql;

public class NpgsqlCoreDbContextFactory : DesignTimeDbContextFactory<NpgsqlCoreDbContext>
{
    protected override NpgsqlCoreDbContext CreateNewInstance(DbContextOptionsBuilder<NpgsqlCoreDbContext> optionsBuilder)
    {
        optionsBuilder.UseNpgsql(Configuration.GetConnectionString("NpgsqlConnection"));
        return new NpgsqlCoreDbContext(optionsBuilder.Options);
    }
}
