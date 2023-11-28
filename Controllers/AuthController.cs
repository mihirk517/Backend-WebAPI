using BCrypt.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebAPI.Models;
using WebAPI.Services.UserAuthService;


namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public static User user = new User();
        private readonly IConfiguration _configuration;
        private readonly IUserAuthService _authService;

        public AuthController(IConfiguration configuration, IUserAuthService authService)
        {
            _configuration = configuration;
            _authService = authService;
        }       


        [HttpPost("register")]
        public async Task<IActionResult> Register(UserDto request)
        {
            var result = await _authService.Register(request);
            if (result == null)
            {
                return BadRequest("User already exists");
            }            
            return Ok(result);
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(UserDto request)
        {
            var result = await _authService.Verify(request);
            if (result == null)
            
            {
                return BadRequest("Invalid Credentials");
            }
           //System.Diagnostics.Debug.WriteLine($"{result.Result.Username}");
            string? token = CreateToken(result);
            System.Diagnostics.Debug.WriteLine($"{token}");
            return Ok(token);
        }

        private string CreateToken(User user)
        {
            System.Diagnostics.Debug.WriteLine(user.Username);
            string jwt =string.Empty;
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

        
    }
}
