using Goal.Domain.Events;
using MediatR;
using Nexus.Core.Domain.Users.Aggregates;

namespace Nexus.Core.Domain.Users.Events;

public class UserAccountCreatedEvent(UserAccount userAccount)
    : Event(userAccount.Id!, nameof(UserAccountCreatedEvent)), INotification
{
    public UserAccount UserAccount { get; } = userAccount;
}
