namespace Nexus.Infra.Crosscutting.Notifications;

public record Notification(string Code, string Message, string? Param = null);