using Nexus.Infra.Crosscutting.Notifications;

namespace Nexus.Infra.Crosscutting.Errors;

public abstract record AppError(ErrorType Type, IEnumerable<Notification> Notifications)
{
    public AppError(ErrorType Type, params Notification[] Notifications)
        : this(Type, Notifications as IEnumerable<Notification>)
    {
    }
}

