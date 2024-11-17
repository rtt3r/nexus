using System.Text.Json;
using Goal.Domain.Events;
using Nexus.Infra.Data.EventSourcing;

namespace Nexus.Infra.Data.EventSourcing.EventStore;

internal class SqlEventStore(EventSourcingDbContext dbContext)
    : IEventStore
{
    protected readonly EventSourcingDbContext dbContext = dbContext;

    public virtual void Save<T>(T @event) where T : IEvent
    {
        var storedEvent = new StoredEvent(
           @event.AggregateId,
           @event.EventType,
           JsonSerializer.Serialize(@event),
           null);

        dbContext.Set<StoredEvent>().Add(storedEvent);
        dbContext.SaveChanges();
    }
}