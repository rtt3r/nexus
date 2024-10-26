using Nexus.Core.Domain.People.Aggregates;

namespace Nexus.Core.Domain.Users.Aggregates;

public class User : NaturalPerson
{
    private User(string id, string name, string username)
        : base(name)
    {
        Id = id;
        Name = name;
        Username = username;
    }

    protected User()
        : base()
    {
    }

    public string Username { get; private set; } = null!;
    public string? Avatar { get; private set; }

    public void UpdateUsername(string username)
        => Username = username;

    public void UpdateAvatar(string avatar)
        => Avatar = avatar;

    public static User CreateUser(string id, string name, string email, string username)
    {
        var user = new User(
            id,
            name,
            username);

        user.AddEmail(email, true);

        return user;
    }
}