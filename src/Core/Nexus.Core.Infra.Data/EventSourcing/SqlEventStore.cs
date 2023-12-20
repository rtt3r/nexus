using System.Text.Json;
using Nexus.Infra.Crosscutting;
using Goal.Seedwork.Domain.Events;

namespace Nexus.Core.Infra.Data.EventSourcing;

public class SqlEventStore(
    EventSourcingDbContext dbContext,
    AppState appState) : IEventStore
{
    private readonly EventSourcingDbContext dbContext = dbContext;
    private readonly AppState appState = appState;

    public void Save<T>(T @event) where T : IEvent
    {
        var storedEvent = new StoredEvent(
           @event.AggregateId,
           JsonSerializer.Serialize(@event),
           appState.User.UserId);

        dbContext.Set<StoredEvent>().Add(storedEvent);
        dbContext.SaveChanges();
    }
}
