using Goal.Domain.Events;
using MediatR;

namespace Nexus.Core.Domain.Transactions.Events;

public sealed class TransactionUpdatedEvent(string aggregateId, string createdBy)
    : Event(aggregateId, nameof(TransactionUpdatedEvent)), INotification
{
    public string CreatedBy { get; } = createdBy;
}
