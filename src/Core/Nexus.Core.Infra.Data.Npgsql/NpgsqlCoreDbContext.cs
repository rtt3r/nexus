using Microsoft.EntityFrameworkCore;

namespace Nexus.Core.Infra.Data.Npgsql;

public class NpgsqlCoreDbContext(DbContextOptions options) : CoreDbContext(options)
{
}
