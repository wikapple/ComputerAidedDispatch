using ComputerAidedDispatchAPI.Data;
using ComputerAidedDispatchAPI.Models;
using ComputerAidedDispatchAPI.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace ComputerAidedDispatchAPI.Repository
{
    public class UnitRepository : Repository<Unit?>, IUnitRepository
    {
        private readonly ComputerAidedDispatchContext _db;

        public UnitRepository(ComputerAidedDispatchContext db) :base(db)
        {
            _db = db;
        }

        public Unit? GetDetails(string unitNumber)
        {
            return _db.Units.Include(a => a.UserInfo).FirstOrDefault(a => a.UnitNumber == unitNumber);

        }

        public async Task<Unit> UpdateAsync(Unit entity)
        {
            _db.Units.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }

    }
}
