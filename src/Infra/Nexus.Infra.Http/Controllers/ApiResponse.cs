using Nexus.Infra.Crosscutting.Notifications;

namespace Nexus.Infra.Http.Controllers;

public record ApiResponse(bool IsSucceeded, params Notification[] Messages)
{
    public static ApiResponse Success()
        => new(true, []);

    public static ApiResponse<TData> Success<TData>(TData data)
        => new(true, data, []);

    public static ApiResponse Fail(IEnumerable<Notification> messages)
        => Fail(messages.ToArray());

    public static ApiResponse Fail(params Notification[] messages)
        => new(false, messages);
}
