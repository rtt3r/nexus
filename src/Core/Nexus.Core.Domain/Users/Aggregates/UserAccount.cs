using Goal.Seedwork.Domain.Aggregates;

namespace Nexus.Core.Domain.Users.Aggregates;

public class UserAccount : Entity<string>
{
    public UserAccount(string id, string email, string username)
        : this()
    {
        Id = id;
        Email = email;
        Username = username;
    }

    protected UserAccount()
        : base()
    { }

    public string Email { get; private set; }
    public string Username { get; private set; }
    public UserProfile Profile { get; private set; }

    public void CreateProfile(string name)
        => Profile = new UserProfile(this, name);
}