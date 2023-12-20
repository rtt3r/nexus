using Goal.Seedwork.Application.Commands;
using Goal.Seedwork.Infra.Crosscutting.Notifications;

namespace Nexus.Infra.Http.Controllers;

public record ApiResponse(bool IsSucceeded, params ApiResponseMessage[] Messages)
{
    public ApiResponse(bool IsSucceeded, params Notification[] notifications)
        : this(IsSucceeded, MapNotificationsToMessageArray(notifications))
    {
    }

    public static ApiResponse Success()
        => new(true, Array.Empty<ApiResponseMessage>());

    public static ApiResponse<TData> Success<TData>(TData data)
        => new(true, data, Array.Empty<ApiResponseMessage>());

    public static ApiResponse Fail(IEnumerable<ApiResponseMessage> messages)
        => Fail(messages.ToArray());

    public static ApiResponse Fail(IEnumerable<Notification> notifications)
        => Fail(notifications.ToArray());

    public static ApiResponse Fail(params Notification[] notifications)
        => Fail(MapNotificationsToMessageArray(notifications));

    public static ApiResponse Fail(params ApiResponseMessage[] messages)
        => new(false, messages);

    public static ApiResponse FromCommand(ICommandResult result)
        => new(result.IsSucceeded, MapNotificationsToMessageArray(result.Notifications));

    public static ApiResponse<TData> FromCommand<TData>(ICommandResult<TData> result)
        => new(result.IsSucceeded, result.Data, MapNotificationsToMessageArray(result.Notifications));

    protected static ApiResponseMessage[] MapNotificationsToMessageArray(IEnumerable<Notification> notifications)
    {
        return (notifications ?? [])
            .Select(n => new ApiResponseMessage(n.Code, n.Message, n.ParamName))
            .ToArray();
    }
}
