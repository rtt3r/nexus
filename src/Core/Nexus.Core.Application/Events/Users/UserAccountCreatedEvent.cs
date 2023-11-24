using Goal.Seedwork.Domain.Events;
using MediatR;
using Nexus.Core.Model.Users;

namespace Nexus.Core.Application.Events.Users;

public class UserAccountCreatedEvent : Event, INotification
{
    public UserAccountCreatedEvent(UserAccount userAccount)
    {
        AggregateId = userAccount.Id;
        UserAccount = userAccount;
    }

    public UserAccount UserAccount { get; private set; }
}
