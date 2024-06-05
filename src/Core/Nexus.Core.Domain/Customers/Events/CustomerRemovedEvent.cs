using Goal.Domain.Events;
using MediatR;

namespace Nexus.Core.Domain.Customers.Events;

public class CustomerRemovedEvent(string aggregateId)
    : Event(aggregateId, nameof(CustomerRemovedEvent)), INotification
{
}
