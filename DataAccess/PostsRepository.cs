using WebAPI.Data;
using WebAPI.DataAccess.IRepository;
using WebAPI.Models;

namespace WebAPI.DataAccess
{
    public class PostsRepository : Repository<Post>, IPostsRepository
    {
        private readonly DatabaseContext _db;
        public PostsRepository(DatabaseContext db) : base(db)
        {
            _db = db;
        }

        void IPostsRepository.Save()
        {
            _db.SaveChanges();
        }

        void IPostsRepository.Update(Post post)
        {
            _db.Posts.Update(post);
        }
    }
}
