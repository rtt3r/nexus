using Goal.Seedwork.Domain.Events;
using MediatR;

namespace Nexus.Core.Application.Events.Users;

public class UserRegisteredEvent : Event, INotification
{
    public UserRegisteredEvent(string aggregateId, string name, string email)
    {
        AggregateId = aggregateId;
        Name = name;
        Email = email;
    }

    public string Name { get; protected set; }
    public string Email { get; protected set; }
}
