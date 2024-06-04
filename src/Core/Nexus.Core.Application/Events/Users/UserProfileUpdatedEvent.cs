using Goal.Domain.Events;
using MediatR;
using Nexus.Core.Model.Users;

namespace Nexus.Core.Application.Events.Users;

public class UserProfileUpdatedEvent(UserAccount userAccount)
    : Event(userAccount.Id!, nameof(UserProfileUpdatedEvent)), INotification
{
    public UserAccount UserAccount { get; } = userAccount;
}