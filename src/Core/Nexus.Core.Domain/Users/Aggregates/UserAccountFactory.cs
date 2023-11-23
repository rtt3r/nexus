namespace Nexus.Core.Domain.Users.Aggregates
{
    public static class UserAccountFactory
    {
        public static UserAccount CreateNewAccount(string id, string name, string email, string username)
        {
            var account = new UserAccount(id, email, username);
            account.CreateProfile(name);

            return account;
        }
    }
}