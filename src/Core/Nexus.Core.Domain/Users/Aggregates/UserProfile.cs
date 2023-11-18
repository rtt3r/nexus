using Goal.Seedwork.Domain.Aggregates;

namespace Nexus.Core.Domain.Users.Aggregates
{
    public class UserProfile : Entity<string>
    {
        public UserProfile(string id)
        {
            Id = id;
        }

        public string Avatar { get; set; }
        public string Biography { get; set; }
        public DateTime? Birthdate { get; set; }
        public string Role { get; set; }
        public UserStatus Status { get; set; } = UserStatus.Online;
    }
}