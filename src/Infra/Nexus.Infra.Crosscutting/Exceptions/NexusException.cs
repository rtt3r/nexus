using System.Net;
using Nexus.Infra.Crosscutting.Notifications;

namespace Nexus.Infra.Crosscutting.Exceptions;

public abstract class NexusException(string message, HttpStatusCode statusCode, params Notification[] notifications)
    : Exception(message)
{
    public HttpStatusCode StatusCode { get; } = statusCode;
    public Notification[] Notifications { get; } = notifications;
}
