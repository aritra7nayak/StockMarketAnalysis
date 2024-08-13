using System.Linq.Expressions;

namespace UserAnalyticsService.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(Guid id);
        Task Add(T entity);
        Task<bool> Update(T entity);
        Task<bool> Delete(Guid id);
        Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate);
    }
}
