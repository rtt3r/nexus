using Goal.Domain.Events;
using MediatR;

namespace Nexus.Core.Application.Events.Customers;

public record CustomerRegisteredEvent(string AggregateId, string Name, string Email, DateTime Birthdate)
    : Event(AggregateId, nameof(CustomerUpdatedEvent)), INotification
{
}
