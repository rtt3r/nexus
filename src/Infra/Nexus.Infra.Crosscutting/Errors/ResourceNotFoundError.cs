using Nexus.Infra.Crosscutting.Notifications;

namespace Nexus.Infra.Crosscutting.Errors;

public record ResourceNotFoundError(IEnumerable<Notification> Notifications)
    : AppError(ErrorType.ResourceNotFound, Notifications)
{
    public ResourceNotFoundError(params Notification[] Notifications)
        : this(Notifications as IEnumerable<Notification>)
    {
    }
}