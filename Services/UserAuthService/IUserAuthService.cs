using WebAPI.Models;

namespace WebAPI.Services.UserAuthService
{
    public interface IUserAuthService
    {
        Task<User> Register(UserDto userDto);
        Task<User> Verify(UserDto userDto);
    }
}
