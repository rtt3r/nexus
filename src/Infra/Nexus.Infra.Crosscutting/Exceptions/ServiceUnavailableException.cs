using System.Net;
using Nexus.Infra.Crosscutting.Notifications;

namespace Nexus.Infra.Crosscutting.Exceptions;

public class ServiceUnavailableException(string message, params Notification[] notifications)
    : NexusException(message, HttpStatusCode.ServiceUnavailable, notifications)
{
}