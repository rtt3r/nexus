using Goal.Domain.Events;
using MediatR;

namespace Nexus.Core.Domain.BusinessGroups.Events;

public sealed class BusinessGroupUpdatedEvent(string aggregateId, string createdBy)
    : Event(aggregateId, nameof(BusinessGroupUpdatedEvent)), INotification
{
    public string CreatedBy { get; } = createdBy;
}
