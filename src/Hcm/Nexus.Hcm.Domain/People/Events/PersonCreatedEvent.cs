using Goal.Domain.Events;
using MediatR;

namespace Nexus.Hcm.Domain.People.Events;

public sealed class PersonCreatedEvent(string aggregateId, string createdBy)
    : Event(aggregateId, nameof(PersonCreatedEvent)), INotification
{
    public string CreatedBy { get; } = createdBy;
}
