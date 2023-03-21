using ComputerAidedDispatchAPI.Models;
using System.Linq.Expressions;

namespace ComputerAidedDispatchAPI.Repository.IRepository
{
    public interface ICallCommentRepository
    {
        // We don't want to be able to update call comments (or delete).
        public Task<CallComment> CreateAsync(CallComment entity);
        public Task<CallComment> GetAsync(Expression<Func<CallComment, bool>> filter = null, bool tracked = true, string? includeProperties = null);
        public Task<List<CallComment>> GetAllAsync(Expression<Func<CallComment, bool>>? filter = null, string? includeProperties = null);
    }
}
