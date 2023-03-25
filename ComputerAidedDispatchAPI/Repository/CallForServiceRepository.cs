using ComputerAidedDispatchAPI.Data;
using ComputerAidedDispatchAPI.Models;
using ComputerAidedDispatchAPI.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

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
            
            _db.CallsForService.Update(entity);
            await SaveAsync();
            return entity;
        }
    }
}
