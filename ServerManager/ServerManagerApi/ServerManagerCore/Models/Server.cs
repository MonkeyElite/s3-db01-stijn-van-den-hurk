using System.ComponentModel.DataAnnotations;

namespace ServerManagerCore.Models
{
    public class Server(string title, string description, string gameName, string ip, int port, string password)
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(255)]
        public string Title { get; set; } = title;

        [Required]
        [MaxLength(10000)]
        public string Description { get; set; } = description;

        [Required]
        [MaxLength(255)]
        public string GameName { get; set; } = gameName;

        [Required]
        [MaxLength(39)]
        public string Ip { get; set; } = ip;

        [Required]
        [Range(1, 65535, ErrorMessage = "Port number must be between 1 and 65535.")]
        public int Port { get; set; } = port;

        [Required]
        [Range(8, 32, ErrorMessage = "Password must be between 8 and 32 characters long.")]
        public string Password { get; set; } = password;
    }
}
