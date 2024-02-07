using WebAPI.Models;

namespace WebAPI.DataAccess.IRepository
{
    public interface IUserRepository :IRepository<User>
    {
        void Update(User user);

        void Save();
    }
}
