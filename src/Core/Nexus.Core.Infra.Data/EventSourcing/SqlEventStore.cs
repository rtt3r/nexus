using System.Text.Json;
using Goal.Domain.Events;

namespace Nexus.Core.Infra.Data.EventSourcing;

public class SqlEventStore(EventSourcingDbContext dbContext)
    : IEventStore
{
    protected readonly EventSourcingDbContext dbContext = dbContext;

    public virtual void Save<T>(T @event) where T : IEvent
    {
        var storedEvent = new StoredEvent(
           @event.AggregateId,
           JsonSerializer.Serialize(@event),
           string.Empty);

        dbContext.Set<StoredEvent>().Add(storedEvent);
        dbContext.SaveChanges();
    }
}