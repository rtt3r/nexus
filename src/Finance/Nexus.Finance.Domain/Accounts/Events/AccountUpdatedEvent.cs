using Goal.Domain.Events;
using MediatR;

namespace Nexus.Finance.Domain.Accounts.Events;

public sealed class AccountUpdatedEvent(string aggregateId, string createdBy)
    : Event(aggregateId, nameof(AccountUpdatedEvent)), INotification
{
    public string CreatedBy { get; } = createdBy;
}
