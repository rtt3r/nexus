using System.Net;
using Nexus.Infra.Crosscutting.Notifications;
using static Nexus.Infra.Crosscutting.Constants.ApplicationConstants;

namespace Nexus.Infra.Crosscutting.Exceptions;

public class ResourceNotFoundException(params Notification[] notifications)
    : NexusException(Messages.RESOURCE_NOT_FOUND, HttpStatusCode.NotFound, notifications)
{
    public ResourceNotFoundException(string code, string message)
        : this(new Notification(code, message))
    {
    }
}