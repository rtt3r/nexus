using Goal.Domain.Events;
using MediatR;

namespace Nexus.Core.Domain.Persons.Events;

public sealed class CompanyCreatedEvent(string aggregateId, string createdBy)
    : Event(aggregateId, nameof(CompanyCreatedEvent)), INotification
{
    public string CreatedBy { get; } = createdBy;
}
