using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        public string? Username { get; set; }
        [Required]
        public string? PasswordHash { get; set; }
        [Timestamp]
        public DateTime? Timestamp { get; set; }
    }
}
