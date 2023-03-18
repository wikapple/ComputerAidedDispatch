using ComputerAidedDispatchAPI.Data;
using ComputerAidedDispatchAPI.Models;
using ComputerAidedDispatchAPI.Repository.IRepository;

namespace ComputerAidedDispatchAPI.Repository
{
    public class DispatcherRepository : Repository<Dispatcher?>, IDispatcherRepository
    {
        private readonly ComputerAidedDispatchContext _db;

        public DispatcherRepository(ComputerAidedDispatchContext db) : base(db)
        {
            _db = db;
        }

        public async Task<Dispatcher> UpdateAsync(Dispatcher entity)
        {
            _db.Dispatchers.Update(entity);
            _db.SaveChangesAsync();
            return entity;
        }
    }
}
