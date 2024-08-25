using MongoDB.Driver;
using System.Linq.Expressions;
using UserAnalyticsService.Repository.IRepository;

namespace UserAnalyticsService.Repository
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        protected readonly IMongoCollection<T> _collection;
        public GenericRepository(IMongoDatabase database, string collectionName)
        {
            _collection = database.GetCollection<T>(collectionName);
        }
        public async Task<IEnumerable<T>> GetAll()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }

        public async Task<T> GetById(Guid id)
        {
            return await _collection.Find(Builders<T>.Filter.Eq("Id", id)).FirstOrDefaultAsync();
        }

        public async Task Add(T entity)
        {
            try
            {
                await _collection.InsertOneAsync(entity);

            }
            catch (Exception ex)
            {

            }
        }

        public async Task<bool> Update(T entity)
        {
            var result = await _collection.ReplaceOneAsync(Builders<T>.Filter.Eq("Id", (entity as dynamic).Id), entity);
            return result.IsAcknowledged && result.ModifiedCount > 0;
        }

        public async Task<bool> Delete(Guid id)
        {
            var result = await _collection.DeleteOneAsync(Builders<T>.Filter.Eq("Id", id));
            return result.IsAcknowledged && result.DeletedCount > 0;
        }

        public async Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate)
        {
            return await _collection.Find(predicate).ToListAsync();
        }
    }
}
