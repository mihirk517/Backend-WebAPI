using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models
{
    public class Post
    {
        public int Id { get; set; }
        public required string Title { get; set; }   
        public required string Content { get; set; }
        public DateTime createdAt { get; set; }
       
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }

    }
}
