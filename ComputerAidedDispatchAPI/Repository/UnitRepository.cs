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
            dbSet.Include(unit => unit.UserInfo);
        }

        public async Task<Unit> UpdateAsync(Unit entity)
        {
            Unit? unit = _db.Units.FirstOrDefault(x => x.UnitNumber == entity.UnitNumber);

            if (unit != null)
            {
                if (unit.Status != entity.Status)
                {
                    entity.TimeStatusAssigned = DateTime.Now;
                }

                _db.Units.Update(entity);
                await _db.SaveChangesAsync();
            }
            return entity;
        }

    }
}
