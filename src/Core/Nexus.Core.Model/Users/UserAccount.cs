namespace Nexus.Core.Model.Users;

public class UserAccount
{
    public string? Id { get; set; }
    public string? Email { get; set; }
    public string? Name { get; set; }
    public string? Username { get; set; }
    public UserProfile? Profile { get; set; }
}