using Goal.Domain.Events;
using MediatR;

namespace Nexus.Core.Application.Events.Customers;

public class CustomerRemovedEvent(string aggregateId)
    : Event(aggregateId, nameof(CustomerRemovedEvent)), INotification
{
}
