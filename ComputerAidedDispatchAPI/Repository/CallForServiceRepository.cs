using ComputerAidedDispatchAPI.Data;
using ComputerAidedDispatchAPI.Models;
using ComputerAidedDispatchAPI.Repository.IRepository;

namespace ComputerAidedDispatchAPI.Repository
{
    public class CallForServiceRepository : Repository<CallForService?>, ICallForServiceRepository
    {

        private readonly ComputerAidedDispatchContext _db;
        public CallForServiceRepository(ComputerAidedDispatchContext db) : base(db)
        {
            _db = db;
        }

        public async Task<CallForService> UpdateAsync(CallForService entity)
        {
            var thisCall = _db.CallsForService.FirstOrDefault(x => x.Id == entity.Id);

            if (thisCall == null && thisCall.Status != entity.Status) 
            {
                entity.TimeStatusAssigned = DateTime.Now;
            }
            
            _db.CallsForService.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
