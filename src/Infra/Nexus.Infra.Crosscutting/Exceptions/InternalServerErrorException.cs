using System.Net;
using Nexus.Infra.Crosscutting.Notifications;

namespace Nexus.Infra.Crosscutting.Exceptions;

public class InternalServerErrorException(string message, params Notification[] notifications)
    : NexusException(message, HttpStatusCode.InternalServerError, notifications)
{
}