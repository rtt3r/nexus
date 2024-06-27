using Goal.Domain.Events;
using MediatR;
using Nexus.Core.Domain.Users.Aggregates;

namespace Nexus.Core.Domain.Users.Events;

public class UserRegisteredEvent(User user, string createdBy)
    : Event(user.Id!, nameof(UserRegisteredEvent)), INotification
{
    public User UserAccount { get; } = user;
    public string CreatedBy { get; } = createdBy;
}
