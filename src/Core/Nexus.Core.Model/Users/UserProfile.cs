namespace Nexus.Core.Model.Users
{
    public class UserProfile
    {
        public string Id { get; set; }
        public string Avatar { get; set; }
        public string Biography { get; set; }
        public DateTime? Birthdate { get; set; }
        public string Role { get; set; }
        public string Status { get; set; }
    }
}