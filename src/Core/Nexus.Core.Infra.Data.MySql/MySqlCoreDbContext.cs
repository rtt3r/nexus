using Microsoft.EntityFrameworkCore;

namespace Nexus.Core.Infra.Data.MySql;

public class MySqlCoreDbContext(DbContextOptions options) : CoreDbContext(options)
{
}
