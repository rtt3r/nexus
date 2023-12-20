using Nexus.Core.Infra.Data.EventSourcing;
using Microsoft.EntityFrameworkCore;

namespace Nexus.Core.Infra.Data.Npgsql;

public class NpgsqlEventSourcingDbContext(DbContextOptions options) : EventSourcingDbContext(options)
{
}
