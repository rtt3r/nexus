using Goal.Infra.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Nexus.Core.Infra.Data.SqlServer;

public class SqlServerCoreDbContextFactory : DesignTimeDbContextFactory<SqlServerCoreDbContext>
{
    protected override SqlServerCoreDbContext CreateNewInstance(DbContextOptionsBuilder<SqlServerCoreDbContext> optionsBuilder)
    {
        optionsBuilder.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
        return new SqlServerCoreDbContext(optionsBuilder.Options);
    }
}
