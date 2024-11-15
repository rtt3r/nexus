using Nexus.Infra.Crosscutting.Notifications;

namespace Nexus.Infra.Crosscutting.Errors;

public record BusinessRuleError(IEnumerable<Notification> Notifications)
    : AppError(ErrorType.BusinessRule, Notifications)
{
    public BusinessRuleError(params Notification[] Notifications)
        : this(Notifications as IEnumerable<Notification>)
    {
    }
}