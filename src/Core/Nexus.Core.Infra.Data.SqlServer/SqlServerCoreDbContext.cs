using Microsoft.EntityFrameworkCore;

namespace Nexus.Core.Infra.Data.SqlServer;

public class SqlServerCoreDbContext(DbContextOptions options) : CoreDbContext(options)
{
}
