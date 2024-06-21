using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;

namespace ServerManagerCore.Models
{
    public class User(string username, string email, string sub)
    {
        public int Id { get; set; }
        public string Username { get; set; } = username;
        public string Email { get; set; } = email;
        public string Sub { get; set; } = sub;

        public ICollection<SessionUser> SessionUsers { get; set; } = new List<SessionUser>();
    }
}
