using Microsoft.EntityFrameworkCore;

namespace Nexus.Core.Infra.Data.SqlServer;

public class SqlServerCoreDbContext(DbContextOptions<SqlServerCoreDbContext> options) : CoreDbContext(options)
{
}
