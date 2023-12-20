namespace Nexus.Infra.Crosscutting.Exceptions;

public class BusinessException(params string[] messages) : ApplicationException("An business error occurs.")
{

    public string[] Messages { get; } = messages;
}
