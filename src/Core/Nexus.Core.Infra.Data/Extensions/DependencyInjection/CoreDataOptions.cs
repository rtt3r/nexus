namespace Nexus.Core.Infra.Data.Extensions.DependencyInjection;

public sealed class CoreDataOptions
{
    public string ConnectionString { get; private set; } = default!;

    public void UseConnectionString(string connectionString)
        => ConnectionString = connectionString;

}