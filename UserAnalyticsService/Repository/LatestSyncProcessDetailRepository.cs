using MongoDB.Driver;
using UserAnalyticsService.Models;
using UserAnalyticsService.Repository.IRepository;

namespace UserAnalyticsService.Repository
{
    public class LatestSyncProcessDetailRepository : GenericRepository<LatestSyncProcessDetail>, ILatestSyncProcessDetailRepository
    {
        private readonly IMongoDatabase _database;

        public LatestSyncProcessDetailRepository(IMongoDatabase database)
            : base(database, "LatestSyncProcessDetails") // "LatestSyncProcessDetails" is the collection name in MongoDB
        {
            _database = database;
        }

        public IMongoCollection<LatestSyncProcessDetail> LatestSyncProcessDetails => _collection;
    }
}