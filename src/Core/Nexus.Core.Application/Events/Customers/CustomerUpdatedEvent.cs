using Goal.Domain.Events;
using MediatR;

namespace Nexus.Core.Application.Events.Customers;

public record CustomerUpdatedEvent(string AggregateId, string Name, string Email, DateTime Birthdate)
    : Event(AggregateId, nameof(CustomerUpdatedEvent)), INotification
{
}
