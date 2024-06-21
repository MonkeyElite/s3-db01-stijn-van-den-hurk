namespace ServerManagerApi.Models.Session
{
    public class UserRegistrationViewModel(string username, string email, string sub)
    {
        public string Username { get; set; } = username;
        public string Email { get; set; } = email;
        public string Sub { get; set; } = sub;
    }
}
