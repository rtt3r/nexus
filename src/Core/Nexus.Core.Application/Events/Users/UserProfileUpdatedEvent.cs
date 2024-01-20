using Goal.Domain.Events;
using MediatR;
using Nexus.Core.Model.Users;

namespace Nexus.Core.Application.Events.Users
{
    public record UserProfileUpdatedEvent(UserAccount UserAccount)
        : Event(UserAccount.Id!, nameof(UserProfileUpdatedEvent)), INotification
    {
    }
}