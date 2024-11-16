namespace Nexus.Core.Infra.Data.DependencyInjection;

public sealed class CoreDataOptions
{
    public string DefaultConnectionString { get; private set; } = null!;
    public string EventSourcingConnectionString { get; private set; } = null!;

    public void UseDefaultConnectionString(string connectionString)
        => DefaultConnectionString = connectionString;

    public void UseEventSourcingConnectionString(string connectionString)
        => EventSourcingConnectionString = connectionString;

}