using Goal.Domain.Events;
using MediatR;

namespace Nexus.Core.Domain.Companies.Events;

public sealed class CompanyDeletedEvent(string aggregateId, string createdBy)
    : Event(aggregateId, nameof(CompanyDeletedEvent)), INotification
{
    public string CreatedBy { get; } = createdBy;
}
