using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models
{
    public class Vote
    {
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
        public int PostId { get; set; }
        [ForeignKey("PostId")]
        public Post Post { get; set; }
    }
}
