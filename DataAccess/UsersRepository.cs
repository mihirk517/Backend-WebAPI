using WebAPI.Data;
using WebAPI.DataAccess.IRepository;
using WebAPI.Models;

namespace WebAPI.DataAccess
{
    public class UsersRepository :Repository<User>,IUserRepository
    {
        private readonly DatabaseContext _db;
        public UsersRepository(DatabaseContext db) : base(db)
        {
            _db = db;
        }

        void IUserRepository.Save()
        {
            _db.SaveChanges();
        }

        void IUserRepository.Update(User user)
        {
            _db.Users.Update(user);
        }
    }
}
