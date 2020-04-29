namespace KrisG.TimeTracker.Models.Users
{
    public class AuthenticatedUserModel
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Token { get; set; }
    }
}