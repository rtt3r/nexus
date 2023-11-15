using Microsoft.EntityFrameworkCore;

namespace Nexus.Core.Infra.Data.MySql;

public class MySqlCoreDbContext : CoreDbContext
{
    public MySqlCoreDbContext(DbContextOptions options)
        : base(options)
    { }
}
