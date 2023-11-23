using Goal.Seedwork.Domain.Aggregates;

namespace Nexus.Core.Domain.Users.Aggregates
{
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

        public string Avatar { get; private set; }
        public string Biography { get; private set; }
        public DateTime? Birthdate { get; private set; }
        public string Headline { get; private set; }
        public UserAccount User { get; private set; }
    }
}