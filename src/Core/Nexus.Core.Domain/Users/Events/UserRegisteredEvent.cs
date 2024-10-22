using Goal.Domain.Events;
using MediatR;

namespace Nexus.Core.Domain.Users.Events;

public class UserRegisteredEvent(string aggregateId, string createdBy)
    : Event(aggregateId, nameof(UserRegisteredEvent)), INotification
{
    public string CreatedBy { get; } = createdBy;
}
