using ComputerAidedDispatchAPI.Data;
using ComputerAidedDispatchAPI.Models;
using ComputerAidedDispatchAPI.Repository.IRepository;

namespace ComputerAidedDispatchAPI.Repository
{
    public class CallCommentRepository : Repository<CallComment>, ICallCommentRepository
    {
        private readonly ComputerAidedDispatchContext _db;

        public CallCommentRepository(ComputerAidedDispatchContext db) : base(db)
        {
            _db = db;   
        }
    }
}
