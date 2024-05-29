using Goal.Domain.Events;

namespace Nexus.Core.Infra.Data.EventSourcing;

public record StoredEvent(string AggregateId, string EventType, string Data, string User)
    : Event(AggregateId, EventType)
{
    public string Id { get; init; } = Guid.NewGuid().ToString();
}
