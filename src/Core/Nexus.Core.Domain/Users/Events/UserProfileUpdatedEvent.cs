using Goal.Domain.Events;
using MediatR;
using Nexus.Core.Domain.Users.Aggregates;

namespace Nexus.Core.Domain.Users.Events;

public class UserProfileUpdatedEvent(UserAccount userAccount, string createdBy)
    : Event(userAccount.Id!, nameof(UserProfileUpdatedEvent)), INotification
{
    public UserAccount UserAccount { get; } = userAccount;
    public string CreatedBy { get; } = createdBy;
}