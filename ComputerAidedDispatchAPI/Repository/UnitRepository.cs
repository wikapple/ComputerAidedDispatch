using ComputerAidedDispatchAPI.Data;
using ComputerAidedDispatchAPI.Models;
using ComputerAidedDispatchAPI.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

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
        public async Task<List<Unit>> GetDetailsForAll(Expression<Func<Unit, bool>>? filter = null)
        {
            IQueryable<Unit> query = dbSet.Include(x => x.CallForService)!;
            if(filter != null)
            {
                query = query.Where(filter);
            }

            return await query.ToListAsync();
        }

        public async Task<Unit> UpdateAsync(Unit entity)
        {
            Unit unit = _db.Units.FirstOrDefault(x => x.UnitNumber == entity.UnitNumber);

            if(unit.Status != entity.Status)
            {
                entity.TimeStatusAssigned = DateTime.Now;
            }

            _db.Units.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }

    }
}
