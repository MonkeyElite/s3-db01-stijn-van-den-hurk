using System.ComponentModel.DataAnnotations;

namespace ServerManagerCore.Models
{
    public class Session(string title, string description, DateTime startTime, DateTime endTime, int serverId)
    {
        [Range(0, int.MaxValue)]
        public int Id { get; set; }
        
        [Required]
        [MaxLength(255)]
        public string Title { get; set; } = title;

        [Required]
        [MaxLength(10000)]
        public string Description { get; set; } = description;

        [Required]
        public DateTime StartTime { get; set; } = startTime;

        [Required]
        public DateTime EndTime { get; set; } = endTime;

        [Required]
        public int ServerId { get; set; } = serverId;

        public ICollection<SessionUser> SessionUsers { get; set; } = new List<SessionUser>();

    }
}
