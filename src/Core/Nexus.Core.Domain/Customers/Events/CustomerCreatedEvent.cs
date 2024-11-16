using Goal.Domain.Events;
using MediatR;

namespace Nexus.Core.Domain.Customers.Events;

public sealed class CustomerCreatedEvent(string aggregateId, string createdBy)
    : Event(aggregateId, nameof(CustomerCreatedEvent)), INotification
{
    public string CreatedBy { get; } = createdBy;
}
