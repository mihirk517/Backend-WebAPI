using WebAPI.Models;

namespace WebAPI.DataAccess.IRepository
{
    public interface IPostsRepository : IRepository<Post>
    {
        void Update(Post post);

        void Save();
    }
}
