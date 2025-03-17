using Goal.Domain.Events;
using MediatR;

namespace Nexus.Core.Domain.Companies.Events;

public sealed class CompanyUpdatedEvent(string aggregateId, string createdBy)
    : Event(aggregateId, nameof(CompanyUpdatedEvent)), INotification
{
    public string CreatedBy { get; } = createdBy;
}
