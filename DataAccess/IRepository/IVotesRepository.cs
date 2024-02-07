using WebAPI.Models;

namespace WebAPI.DataAccess.IRepository
{
    public interface IVotesRepository :IRepository<Vote>
    { 
        public void Update(Vote vote);
        public void Save();
    }
}
