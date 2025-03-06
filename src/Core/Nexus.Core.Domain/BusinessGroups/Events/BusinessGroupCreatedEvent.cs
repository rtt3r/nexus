using Goal.Domain.Events;
using MediatR;

namespace Nexus.Core.Domain.BusinessGroups.Events;

public sealed class BusinessGroupCreatedEvent(string aggregateId, string createdBy)
    : Event(aggregateId, nameof(BusinessGroupCreatedEvent)), INotification
{
    public string CreatedBy { get; } = createdBy;
}
