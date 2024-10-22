using Goal.Domain.Events;
using MediatR;

namespace Nexus.Core.Domain.Users.Events;

public class UserRegisteredEvent : Event, INotification
{
    public UserRegisteredEvent(string aggregateId, string createdBy)
        : base(aggregateId, nameof(UserRegisteredEvent))
    {
        CreatedBy = createdBy;
    }

    public string CreatedBy { get; }
}
