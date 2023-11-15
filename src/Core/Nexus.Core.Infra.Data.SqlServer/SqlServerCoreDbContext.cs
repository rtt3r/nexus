using Microsoft.EntityFrameworkCore;

namespace Nexus.Core.Infra.Data.SqlServer;

public class SqlServerCoreDbContext : CoreDbContext
{
    public SqlServerCoreDbContext(DbContextOptions options)
        : base(options)
    { }
}
