using Microsoft.EntityFrameworkCore;

namespace Nexus.Core.Infra.Data.MySql;

public class MySqlCoreDbContext(DbContextOptions<MySqlCoreDbContext> options) : CoreDbContext(options)
{
}
