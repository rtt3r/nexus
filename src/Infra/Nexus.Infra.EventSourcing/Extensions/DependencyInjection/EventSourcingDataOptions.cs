namespace Nexus.Infra.EventSourcing.Extensions.DependencyInjection;

public sealed class EventSourcingDataOptions
{
    public string ConnectionString { get; private set; } = null!;

    public void UseConnectionString(string connectionString)
        => ConnectionString = connectionString;
}