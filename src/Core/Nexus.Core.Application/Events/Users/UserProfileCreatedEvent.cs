using Goal.Seedwork.Domain.Events;
using MediatR;

namespace Nexus.Core.Application.Events.Users;

public class UserProfileCreatedEvent : Event, INotification
{
    public UserProfileCreatedEvent(string userId)
    {
        AggregateId = userId;
    }
}
