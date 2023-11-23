using Goal.Seedwork.Domain.Events;
using MediatR;

namespace Nexus.Core.Application.Events.Users;

public class UserAccountCreatedEvent : Event, INotification
{
    public UserAccountCreatedEvent(string aggregateId, string email, string name, string username)
    {
        AggregateId = aggregateId;
        Email = email;
        Name = name;
        Username = username;
    }

    public string Email { get; private set; }
    public string Name { get; private set; }
    public string Username { get; private set; }
}
