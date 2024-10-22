using Goal.Domain.Events;
using MediatR;

namespace Nexus.Core.Domain.Customers.Events;

public class CustomerRegisteredEvent(string aggregateId, string createdBy)
    : Event(aggregateId, nameof(CustomerRegisteredEvent)), INotification
{
    public string CreatedBy { get; } = createdBy;
}
