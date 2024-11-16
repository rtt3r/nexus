using Goal.Domain.Events;
using MediatR;

namespace Nexus.Core.Domain.Customers.Events;

public sealed class CustomerUpdatedEvent(string aggregateId, string createdBy)
    : Event(aggregateId, nameof(CustomerUpdatedEvent)), INotification
{
    public string CreatedBy { get; } = createdBy;
}
