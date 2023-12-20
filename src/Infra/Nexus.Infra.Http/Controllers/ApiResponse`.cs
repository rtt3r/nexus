using Goal.Seedwork.Infra.Crosscutting.Notifications;

namespace Nexus.Infra.Http.Controllers;

public record ApiResponse<TData>(bool IsSucceeded, TData? Data, params ApiResponseMessage[] Messages)
    : ApiResponse(IsSucceeded, Messages)
{
    public ApiResponse(bool isSucceeded, TData data, params Notification[] notifications)
        : this(isSucceeded, data, MapNotificationsToMessageArray(notifications))
    {
    }
}
