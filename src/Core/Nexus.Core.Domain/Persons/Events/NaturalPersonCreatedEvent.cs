using Goal.Domain.Events;
using MediatR;

namespace Nexus.Core.Domain.Persons.Events;

public sealed class NaturalPersonCreatedEvent(string aggregateId, string createdBy)
    : Event(aggregateId, nameof(NaturalPersonCreatedEvent)), INotification
{
    public string CreatedBy { get; } = createdBy;
}
