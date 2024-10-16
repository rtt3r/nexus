using System.Net;
using Nexus.Infra.Crosscutting.Notifications;
using static Nexus.Infra.Crosscutting.Constants.ApplicationConstants;

namespace Nexus.Infra.Crosscutting.Exceptions;

public class RequestValidationException(params Notification[] notifications)
    : NexusException(Messages.REQUEST_VALIDATION, HttpStatusCode.BadRequest, notifications)
{
}