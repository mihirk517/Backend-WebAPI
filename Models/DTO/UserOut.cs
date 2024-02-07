using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models.DTO
{
    public class UserOut
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public DateTime? Timestamp { get; set; }
    }
}
