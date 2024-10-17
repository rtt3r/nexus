using System.Text.Json.Serialization;
using Goal.Domain.Aggregates;

namespace Nexus.Core.Domain.Users.Aggregates;

public class User : Entity
{
    public User(string id, string name, string email, string username)
        : this()
    {
        Id = id;
        Name = name;
        Email = email;
        Username = username;
    }

    protected User()
        : base()
    {
    }

    public string Name { get; private set; } = null!;
    public string Email { get; private set; } = null!;
    public string Username { get; private set; } = null!;
    public string? Avatar { get; private set; }

    public void UpdateAvatar(string avatar)
        => Avatar = avatar;

    public static User CreateUser(string id, string name, string email, string username)
    {
        return new User(
            id,
            name,
            email,
            username);
    }
}