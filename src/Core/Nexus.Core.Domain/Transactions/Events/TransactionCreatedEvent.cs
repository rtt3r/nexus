using Goal.Domain.Events;
using MediatR;

namespace Nexus.Core.Domain.Transactions.Events;

public sealed class TransactionCreatedEvent(string aggregateId, string createdBy)
    : Event(aggregateId, nameof(TransactionCreatedEvent)), INotification
{
    public string CreatedBy { get; } = createdBy;
}
