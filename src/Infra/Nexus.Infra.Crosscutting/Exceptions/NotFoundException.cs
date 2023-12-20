namespace Nexus.Infra.Crosscutting.Exceptions;

public class NotFoundException(string message) : ApplicationException(message)
{
}
