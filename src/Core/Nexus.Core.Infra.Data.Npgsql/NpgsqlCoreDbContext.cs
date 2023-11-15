using Microsoft.EntityFrameworkCore;

namespace Nexus.Core.Infra.Data.Npgsql;

public class NpgsqlCoreDbContext : CoreDbContext
{
    public NpgsqlCoreDbContext(DbContextOptions options)
        : base(options)
    { }
}
