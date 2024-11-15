using Nexus.Infra.Crosscutting.Notifications;

namespace Nexus.Infra.Crosscutting.Errors;

public record ServiceUnavailableError(IEnumerable<Notification> Notifications)
    : AppError(ErrorType.ServiceUnavailable, Notifications)
{
    public ServiceUnavailableError(params Notification[] Notifications)
        : this(Notifications as IEnumerable<Notification>)
    {
    }
}