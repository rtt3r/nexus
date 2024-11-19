namespace Nexus.Infra.Data.EventSourcing.Extensions.DependencyInjection;

public sealed class EventSourcingDataOptions
{
    public string ConnectionString { get; private set; } = default!;

    public void UseConnectionString(string connectionString)
        => ConnectionString = connectionString;
}