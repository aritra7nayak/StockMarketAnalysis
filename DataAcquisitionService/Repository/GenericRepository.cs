using DataAcquisitionService.Data;
using DataAcquisitionService.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace DataAcquisitionService.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        public readonly AppDbContext _context;
        public readonly DbSet<T> _dbSet;

        public GenericRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteForParent<TChild>(int parentId, string parentIdFieldName) where TChild : class
        {
            var parentEntity = await _context.Set<T>().FindAsync(parentId);
            if (parentEntity != null)
            {
                var childEntities = await _context.Set<TChild>()
                                                  .Where(c => EF.Property<int>(c, parentIdFieldName) == parentId)
                                                  .ToListAsync();

                _context.Set<TChild>().RemoveRange(childEntities);
                _context.Set<T>().Remove(parentEntity);
                await _context.SaveChangesAsync();
            }
        }


    }
}