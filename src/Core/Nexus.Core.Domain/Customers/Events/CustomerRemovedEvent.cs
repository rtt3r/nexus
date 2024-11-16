using Goal.Domain.Events;
using MediatR;

namespace Nexus.Core.Domain.Customers.Events;

public sealed class CustomerRemovedEvent(string aggregateId, string createdBy)
    : Event(aggregateId, nameof(CustomerRemovedEvent)), INotification
{
    public string CreatedBy { get; } = createdBy;
}
