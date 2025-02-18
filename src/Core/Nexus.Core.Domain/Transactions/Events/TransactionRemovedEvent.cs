using Goal.Domain.Events;
using MediatR;

namespace Nexus.Core.Domain.Transactions.Events;

public sealed class TransactionRemovedEvent(string aggregateId, string createdBy)
    : Event(aggregateId, nameof(TransactionRemovedEvent)), INotification
{
    public string CreatedBy { get; } = createdBy;
}
