using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WebAPI.DataAccess.IRepository;
using WebAPI.Models;
using WebAPI.Models.DTO;

namespace WebAPI.Controllers
{
    [ApiController]
    
    public class PostsController : ControllerBase
    {
        private readonly IPostsRepository _postsRepository;
        private readonly IUserRepository _usersRepository;
        private readonly IConfiguration _configuration;        
        public PostsController(IConfiguration configuration, IPostsRepository postsRepository, IUserRepository userRepository)
        {
            _postsRepository = postsRepository;
            _configuration = configuration;
            _usersRepository = userRepository;
        }
        [HttpPost("posts")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize]
        public IActionResult CreatePost(PostCreate request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            System.Diagnostics.Debug.WriteLine($"User Name {User.Identity.Name}");
            User user = _usersRepository.Get(x => x.Username == User.Identity.Name);
            if (user == null)
            {
                return NotFound("Invalid User");
            }
            Post post = new Post()
            {
                Title = request.Title,
                Content = request.Content,
                UserId = user.Id
            };
            _postsRepository.Add(post);
            _postsRepository.Save();
            return Ok(MapPost(post,user));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize]
        public IActionResult GetPost(int? id)
        {
            Post post = _postsRepository.Get(x =>x.Id == id,includeprops:nameof(User));
            if (post == null)
            {
                return NotFound("Post not found");
            }
            
            return Ok(MapPost(post,post.User));

        }

        [HttpGet("posts")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        
        public IActionResult GetAllPosts()
        {
            List<Post> posts = _postsRepository.GetAll(includeprops:nameof(User)).ToList();            
            List<PostDto> postsDto = new List<PostDto>();
            foreach (Post post in posts)
            {
                postsDto.Add(MapPost(post, post.User));
            }

            return Ok(postsDto);
        }
        [Authorize]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult DeletePost(int? id)
        {
            Post post = _postsRepository.Get(x => x.Id == id,includeprops:nameof(User));
            if (post == null)
            {
                return NotFound("Post not found");
            }
            if(post.User.Username != User.Identity?.Name)
            {
                return Unauthorized($"Post with {post.Id} doesn't belong to {post.User.Username}");
            }
            _postsRepository.Remove(post);
            _postsRepository.Save();
            return Ok("Delete Sucessfull");

        }
        [Authorize]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult UpdatePost(int? id, PostCreate request)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Post post = _postsRepository.Get(x => x.Id == id, includeprops: nameof(User));
            if (post == null)
            {
                return NotFound("Post not found");
            }
            if (post.User.Username != User.Identity?.Name)
            {
                return Unauthorized($"Post with Id :{post.Id} doesn't belong to {User.Identity?.Name}");
            }
            post.Title = request.Title;
            post.Content = request.Content;
            _postsRepository.Update(post);
            _postsRepository.Save();
            return Ok(MapPost(post,post.User));

        }
        private PostDto MapPost(Post post,User user)
        {
            UserOut userDto = new UserOut()
            {
                Id = user.Id,
                Username = user.Username,
                Timestamp = user.Timestamp
            };

            PostDto response = new PostDto()
            {
                Id = post.Id,
                Title = post.Title,
                Content = post.Content,
                UserId = user.Id,
                UserOut = userDto
            };
            return response;
        }
    }
}
