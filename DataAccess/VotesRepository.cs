using WebAPI.Data;
using WebAPI.DataAccess.IRepository;
using WebAPI.Models;

namespace WebAPI.DataAccess
{
    public class VotesRepository :Repository<Vote>, IVotesRepository
    {
        private readonly DatabaseContext _db;
        public VotesRepository(DatabaseContext db) :base(db)
        { 
            _db = db;
        }        

        void IVotesRepository.Save()
        {
            _db.SaveChanges();
        }

        void IVotesRepository.Update(Vote vote)
        {
            _db.Votes.Update(vote);
        }
    }
}
