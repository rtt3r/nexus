using Goal.Domain.Events;
using MediatR;

namespace Nexus.Core.Domain.BusinessGroups.Events;

public sealed class BusinessGroupDeletedEvent(string aggregateId, string createdBy)
    : Event(aggregateId, nameof(BusinessGroupDeletedEvent)), INotification
{
    public string CreatedBy { get; } = createdBy;
}
