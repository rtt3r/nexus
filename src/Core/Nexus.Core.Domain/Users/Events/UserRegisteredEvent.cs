using System.Text.Json.Serialization;
using Goal.Domain.Events;
using MediatR;
using Nexus.Core.Domain.Users.Aggregates;

namespace Nexus.Core.Domain.Users.Events;

public class UserRegisteredEvent : Event, INotification
{
    public UserRegisteredEvent(User user, string createdBy)
        : base(user.Id!, nameof(UserRegisteredEvent))
    {
        User = user;
        CreatedBy = createdBy;
    }

    public User User { get; }
    public string CreatedBy { get; }
}
