using Goal.Domain.Events;
using MediatR;

namespace Nexus.Core.Domain.Customers.Events;

public class CustomerRegisteredEvent(string aggregateId, string name, string email, DateOnly birthdate, string createdBy)
    : Event(aggregateId, nameof(CustomerRegisteredEvent)), INotification
{
    public string Name { get; } = name;
    public string Email { get; } = email;
    public DateOnly Birthdate { get; } = birthdate;
    public string CreatedBy { get; } = createdBy;
}
