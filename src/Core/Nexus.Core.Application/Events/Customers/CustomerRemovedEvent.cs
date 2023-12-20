using Goal.Seedwork.Domain.Events;
using MediatR;

namespace Nexus.Core.Application.Events.Customers;

public record CustomerRemovedEvent(string AggregateId)
    : Event(AggregateId, nameof(CustomerRemovedEvent)), INotification
{
}
