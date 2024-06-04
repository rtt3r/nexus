using Goal.Domain.Events;
using MediatR;
using Nexus.Core.Model.Users;

namespace Nexus.Core.Application.Events.Users;

public class UserAccountCreatedEvent(UserAccount userAccount)
    : Event(userAccount.Id!, nameof(UserAccountCreatedEvent)), INotification
{
    public UserAccount UserAccount { get; } = userAccount;
}
