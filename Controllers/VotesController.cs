using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.DataAccess.IRepository;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    //[Route("votes")]
    //[ApiController]
    [Authorize]
    public class VotesController : ControllerBase
    {
        private readonly IPostsRepository _postsRepository;
        private readonly IVotesRepository _votesRepository;
        private readonly IUserRepository _usersRepository;
        public VotesController(IPostsRepository postsRepository, IVotesRepository votesRepository, IUserRepository userRepository)
        {
            _postsRepository = postsRepository;
            _votesRepository = votesRepository;
            _usersRepository = userRepository;
        }
       /* [HttpPost("{PostId}")]
        public IActionResult Vote(int PostId)
        {
            Vote foundVote = _votesRepository.Get(x => x.PostId == PostId, includeprops: "User,Post");
            if (foundVote == null)
            {
                Vote newVote = new Vote();
                {
                    newVote.PostId = PostId;
                    newVote.UserId = GetUserIdbyUsername(User.Identity?.Name);
                }
                
                _votesRepository.Add(newVote);
                _votesRepository.Save();
                return Ok(newVote);
            }

            if(foundVote.Post == null)
            {
                return NotFound("Post Not Found");
            }           
            if(foundVote.User.Username == User.Identity?.Name)
            {
                return Conflict($"{foundVote.User.Username} has already voted");
            }
            return Ok(foundVote);         

        }*/

        private int GetUserIdbyUsername(string? username)
        {
            return _usersRepository.Get(x => x.Username == username).Id;
        }
    }
}
