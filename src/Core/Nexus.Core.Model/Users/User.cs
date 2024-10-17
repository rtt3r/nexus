namespace Nexus.Core.Model.Users;

public class User
{
    public string UserId { get; set; } = null!;
    public string Username { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string? Avatar { get; set; }
}