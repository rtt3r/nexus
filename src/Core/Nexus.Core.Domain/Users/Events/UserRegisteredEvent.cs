using Goal.Domain.Events;
using MediatR;

namespace Nexus.Core.Domain.Users.Events;

public class UserRegisteredEvent : Event, INotification
{
    public UserRegisteredEvent(string aggregateId, string name, string email, string username, string? avatar, string createdBy)
        : base(aggregateId, nameof(UserRegisteredEvent))
    {
        Name = name;
        Email = email;
        Username = username;
        Avatar = avatar;
        CreatedBy = createdBy;
    }

    public string Name { get; private set; } = null!;
    public string Email { get; private set; } = null!;
    public string Username { get; private set; } = null!;
    public string? Avatar { get; private set; }
    public string CreatedBy { get; }
}
