namespace ServerManagerCore.Models
{
    public class SessionUser
    {
        public int SessionId { get; set; }
        public Session Session { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}
