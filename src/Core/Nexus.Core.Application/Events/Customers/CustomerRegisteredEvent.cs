using Goal.Domain.Events;
using MediatR;

namespace Nexus.Core.Application.Events.Customers;

public class CustomerRegisteredEvent(string aggregateId, string name, string email, DateOnly birthdate)
    : Event(aggregateId, nameof(CustomerUpdatedEvent)), INotification
{
    public string Name { get; } = name;
    public string Email { get; } = email;
    public DateOnly Birthdate { get; } = birthdate;
}
