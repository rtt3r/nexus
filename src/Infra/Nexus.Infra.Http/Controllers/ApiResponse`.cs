using Nexus.Infra.Crosscutting.Notifications;

namespace Nexus.Infra.Http.Controllers;

public record ApiResponse<TData>(bool IsSucceeded, TData? Data, params Notification[] Messages)
    : ApiResponse(IsSucceeded, Messages)
{
}
