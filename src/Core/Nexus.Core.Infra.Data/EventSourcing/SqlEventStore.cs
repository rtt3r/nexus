using System.Text.Json;
using Nexus.Infra.Crosscutting;
using Goal.Seedwork.Domain.Events;

namespace Nexus.Core.Infra.Data.EventSourcing;

public class SqlEventStore : IEventStore
{
    private readonly EventSourcingDbContext dbContext;
    private readonly AppState appState;

    public SqlEventStore(
        EventSourcingDbContext dbContext,
        AppState appState)
    {
        this.dbContext = dbContext;
        this.appState = appState;
    }

    public void Save<T>(T @event) where T : IEvent
    {
        var storedEvent = new StoredEvent(
           @event,
           JsonSerializer.Serialize(@event),
           appState.User.UserId);

        dbContext.StoredEvents.Add(storedEvent);
        dbContext.SaveChanges();
    }
}
