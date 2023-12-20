using Goal.Seedwork.Domain.Events;
using MediatR;
using Nexus.Core.Model.Users;

namespace Nexus.Core.Application.Events.Users;

public record UserAccountCreatedEvent(UserAccount UserAccount)
    : Event(UserAccount.Id!, nameof(UserAccountCreatedEvent)), INotification
{
}
