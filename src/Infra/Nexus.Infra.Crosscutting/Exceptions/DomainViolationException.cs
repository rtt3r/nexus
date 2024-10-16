using System.Net;
using Nexus.Infra.Crosscutting.Notifications;
using static Nexus.Infra.Crosscutting.Constants.ApplicationConstants;

namespace Nexus.Infra.Crosscutting.Exceptions;

public class DomainViolationException(params Notification[] notifications)
    : NexusException(Messages.DOMAIN_VIOLATION, HttpStatusCode.UnprocessableEntity, notifications)
{
    public DomainViolationException(string code, string message)
        : this(new Notification(code, message))
    {
    }
}