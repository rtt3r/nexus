namespace Nexus.Core.Model.Users;

public class UserProfile
{
    public string? Avatar { get; set; }
    public string? Biography { get; set; }
    public DateTimeOffset? Birthdate { get; set; }
    public string? Headline { get; set; }
}