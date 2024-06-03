namespace Nexus.Core.Api.Controllers.Users;

public class UpdateUserProfileRequest
{
    public string? Biography { get; set; } = null!;
    public DateOnly? Birthdate { get; set; }
    public string? Headline { get; set; } = null!;
}