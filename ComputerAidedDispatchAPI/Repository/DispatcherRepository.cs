using System.Linq.Expressions;
using ComputerAidedDispatchAPI.Data;
using ComputerAidedDispatchAPI.Models;
using ComputerAidedDispatchAPI.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace ComputerAidedDispatchAPI.Repository;

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
        await _db.SaveChangesAsync();
        return entity;
    }

    public async Task<Dispatcher?> GetDetails(string dispatcherNumber)
    {
        return await _db.Dispatchers
            .Include(x => x.UserInfo)
            .FirstOrDefaultAsync(x => x.DispatcherNumber == dispatcherNumber);
    }

    public async Task<List<Dispatcher>> GetAllDetailsAsync(Expression<Func<Dispatcher, bool>>? filter = null)
    {
        IQueryable<Dispatcher> query = dbSet.Include(x => x.UserInfo)!;

        if(filter != null)
        {
            query = query.Where(filter);
        }

        return await query.ToListAsync();
        
    }


}

