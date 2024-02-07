using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models.DTO
{
    public class PostDto
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Content { get; set; }
        public int UserId { get; set; }
        public UserOut UserOut { get; set; }


    }
}
