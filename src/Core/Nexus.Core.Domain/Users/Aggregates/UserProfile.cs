using Goal.Domain.Aggregates;

namespace Nexus.Core.Domain.Users.Aggregates;

public class UserProfile : Entity<string>
{
    public UserProfile(UserAccount user)
        : this()
    {
        Id = user.Id;
        User = user;
    }

    protected UserProfile()
        : base()
    { }

    public string? Avatar { get; private set; }
    public string? Biography { get; private set; }
    public DateTime? Birthdate { get; private set; }
    public string? Headline { get; private set; }
    public UserAccount User { get; private set; } = null!;

    public void UpdateBiography(string biography)
        => Biography = biography;

    public void UpdateBirthdate(DateTime? birthdate)
        => Birthdate = birthdate;

    public void UpdateHeadline(string headline)
        => Headline = headline;

    public void UpdateAvatar(string avatar)
        => Avatar = avatar;
}