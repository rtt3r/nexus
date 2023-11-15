using Goal.Seedwork.Domain.Events;

namespace Nexus.Core.Application.Events.Customers;

public class CustomerRemovedEvent : Event
{
    public CustomerRemovedEvent(string aggregateId)
    {
        AggregateId = aggregateId;
    }
}
