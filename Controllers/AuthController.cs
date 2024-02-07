using Azure.Core;
using BCrypt.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebAPI.Data;
using WebAPI.DataAccess.IRepository;
using WebAPI.Models;
using WebAPI.Models.DTO;
using WebAPI.Services.UserAuthService;


namespace WebAPI.Controllers
{
    [Route("user")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;
        private readonly IHttpContextAccessor _httpContext;
        private readonly DatabaseContext _db;

        public AuthController(IConfiguration configuration, DatabaseContext db,IUserRepository userRepository,IHttpContextAccessor httpContextAccessor)
        {
            _configuration = configuration;            
            _db = db;
            _userRepository = userRepository;
            _httpContext = httpContextAccessor;
        }


        [HttpPost("register")]
        public IActionResult Register(UserDto request)
        {            
            if (_userRepository.Get(x => x.Username == request.Username) != null)
            {
                Debug.WriteLine($"{request.Username} already present");
                return BadRequest("User already exists");
            }

            User user = new User();
            user.Username = request.Username;
            user.PasswordHash = CreatePasswordHash(request.Password);
            _userRepository.Add(user);
            _userRepository.Save();

            
            return Ok(UserDataOut(user));
        }
        [HttpPost("login")]
        public IActionResult Login([FromForm]UserDto request)
        {
            User user = _userRepository.Get(x => x.Username == request.Username);
            
            if (user == null)
            {
                return BadRequest("Invalid Credentials");
            }
            if(!VerifyPasswordHash(request.Password, user.PasswordHash))
            {
                return BadRequest("Invalid Credentials");
            }
            
            string? token = CreateToken(user);
            System.Diagnostics.Debug.WriteLine($"{token}");
            return Ok(token);
        }
        
        [HttpGet("{id}")]
        public IActionResult GetUser(int id)
        {
            User user = _userRepository.Get(x => x.Id == id);
            if(user == null)
            {
                return BadRequest("User Doesn't exist");
            }
            return Ok(UserDataOut(user));
        }

        private UserOut UserDataOut(User user)
        {
            UserOut response = new UserOut();
            response.Id = user.Id;
            response.Username = user.Username;
            response.Timestamp = user.Timestamp;
            return response;
        }

        private string CreateToken(User user)
        {
            System.Diagnostics.Debug.WriteLine(user.Username);
            string jwt = string.Empty;
            try
            {
                List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name,user.Username)
            };

                SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                    _configuration.GetSection("AppSettings:SecretKey").Value!));

                SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

                JwtSecurityToken token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddSeconds(1),
                    signingCredentials: credentials
                    );

                jwt = new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine($"{e.Message}: {e.StackTrace}");
                throw;
            }


            return jwt;
        }

        private string CreatePasswordHash(string password)
        {
            string passwordHash = BCrypt.Net.BCrypt.EnhancedHashPassword(password);
            return passwordHash;
        }

        private bool VerifyPasswordHash(string password, string? passwordHash)
        {
            return BCrypt.Net.BCrypt.EnhancedVerify(password, passwordHash);
        }


    }
}
