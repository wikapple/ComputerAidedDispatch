using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Linq;
using ComputerAidedDispatchAPI.Data;
using ComputerAidedDispatchAPI.Repository.IRepository;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ComputerAidedDispatchAPI.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ComputerAidedDispatchContext _db;

        internal DbSet<T> dbSet;

        public Repository(ComputerAidedDispatchContext db)
        {
            _db = db;
            this.dbSet = _db.Set<T>();
        }

        public async Task<T> CreateAsync(T entity)
        {
            await dbSet.AddAsync(entity);

            await SaveAsync();

            return entity;
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> filter = null, bool tracked = true, string? includeProperties = null)
        {
            IQueryable<T> query = dbSet;

            if (!tracked)
            {
                query = query.AsNoTracking();
            }


            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProperties != null)
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }
            
            return await query.FirstOrDefaultAsync();
        }

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, string? includeProperties = null)
        {
            IQueryable<T> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }
          
            
            if (includeProperties != null)
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }

            return await query.ToListAsync();
        }

        public async Task RemoveAsync(T entity)
        {
            dbSet.Remove(entity);

            await SaveAsync();
        }

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}
