namespace Nexus.Infra.Http.Controllers;

public record ApiResponseMessage(string Code, string Message, string? Param = null)
{
}
