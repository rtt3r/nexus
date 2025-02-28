using Goal.Domain.Events;
using MediatR;

namespace Nexus.Core.Domain.Accounts.Events;

public sealed class AccountRemovedEvent(string aggregateId, string createdBy)
    : Event(aggregateId, nameof(AccountRemovedEvent)), INotification
{
    public string CreatedBy { get; } = createdBy;
}
