using Goal.Seedwork.Domain.Events;

namespace Nexus.Core.Infra.Data.EventSourcing;

public record StoredEvent(string AggregateId, string Data, string User)
    : Event(AggregateId, nameof(StoredEvent))
{
    public string Id { get; init; } = Guid.NewGuid().ToString();
}
