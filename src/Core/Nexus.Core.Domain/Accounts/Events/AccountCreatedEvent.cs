using Goal.Domain.Events;
using MediatR;

namespace Nexus.Core.Domain.Accounts.Events;

public sealed class AccountCreatedEvent(string aggregateId, string createdBy)
    : Event(aggregateId, nameof(AccountCreatedEvent)), INotification
{
    public string CreatedBy { get; } = createdBy;
}
