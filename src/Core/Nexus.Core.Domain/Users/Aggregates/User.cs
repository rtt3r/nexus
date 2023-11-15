using Goal.Seedwork.Domain.Aggregates;

namespace Nexus.Core.Domain.Users.Aggregates
{
    public class User : Entity<string>
    {
        public User(string name, string email)
        {
            Id = Guid.NewGuid().ToString();
            Name = name;
            Email = email;
        }

        // Empty constructor for EF
        protected User() { }

        public string Name { get; protected set; }

        public string Email { get; protected set; }

        public string Avatar { get; protected set; }

        public UserStatus? Status { get; protected set; }
    }
}