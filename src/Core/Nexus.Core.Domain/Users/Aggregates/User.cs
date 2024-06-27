using Goal.Domain.Aggregates;
using Nexus.Core.Domain.People.Aggregates;

namespace Nexus.Core.Domain.Users.Aggregates;

public class User : Entity
{
    public User(string id, string email, string username)
        : this()
    {
        Id = id;
        Email = email;
        Username = username;
    }

    protected User()
        : base()
    {
    }

    public string Email { get; private set; } = null!;
    public string Username { get; private set; } = null!;
    public string? Avatar { get; private set; }
    public NaturalPerson? Person { get; private set; }

    public void UpdateAvatar(string avatar)
        => Avatar = avatar;
}