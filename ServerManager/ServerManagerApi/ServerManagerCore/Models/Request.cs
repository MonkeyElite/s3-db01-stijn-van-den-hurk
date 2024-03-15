using System.ComponentModel.DataAnnotations;

namespace ServerManagerCore.Models
{
    public class Request(string title, string description)
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(255)]
        public string Title { get; set; } = title;

        [Required]
        [MaxLength(10000)]
        public string Description { get; set; } = description;
    }
}
