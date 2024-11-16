using Goal.Domain.Events;

namespace Nexus.Core.Infra.Data.EventStore;

public class StoredEvent(string aggregateId, string eventType, string data, string? user)
    : Event(aggregateId, eventType)
{
    public string Id { get; init; } = Guid.NewGuid().ToString();
    public string Data { get; } = data;
    public string? User { get; } = user;

    // Empty constructor for EF
    protected StoredEvent()
        : this(null!, null!, null!, null)
    {
    }
}