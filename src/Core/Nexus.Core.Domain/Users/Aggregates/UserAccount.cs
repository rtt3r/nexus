using Goal.Seedwork.Domain.Aggregates;

namespace Nexus.Core.Domain.Users.Aggregates;

public class UserAccount : Entity<string>
{
    public UserAccount(string id, string email, string name, string username)
        : this()
    {
        Id = id;
        Email = email;
        Name = name;
        Username = username;
    }

    protected UserAccount()
        : base()
    {
        Profile = new(this);
    }

    public string Email { get; private set; }
    public string Name { get; private set; }
    public string Username { get; private set; }
    public UserProfile Profile { get; private set; }
}