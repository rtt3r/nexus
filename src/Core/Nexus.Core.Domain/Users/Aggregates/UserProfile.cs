using Goal.Seedwork.Domain.Aggregates;

namespace Nexus.Core.Domain.Users.Aggregates
{
    public class UserProfile : Entity<string>
    {
        public UserProfile(UserAccount user, string name)
            : this()
        {
            Id = user.Id;
            User = user;
            Name = name;
        }

        protected UserProfile()
            : base()
        { }

        public string Name { get; private set; }
        public string Avatar { get; private set; }
        public string Biography { get; private set; }
        public DateTime? Birthdate { get; private set; }
        public string Headline { get; private set; }
        public UserAccount User { get; private set; }
    }
}