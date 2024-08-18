using MongoDB.Driver;
using UserAnalyticsService.Models;
using UserAnalyticsService.Repository.IRepository;

namespace UserAnalyticsService.Repository
{
    public class PriceSyncRunRepository : GenericRepository<PriceSyncRun>, IPriceSyncRunRepository
    {
        private readonly IMongoDatabase _database;

        public PriceSyncRunRepository(IMongoDatabase database)
            : base(database, "PriceSyncRuns") // "PriceSyncRuns" is the collection name in MongoDB
        {
            _database = database;
        }

        public IMongoCollection<PriceSyncRun> PriceSyncRuns => _collection;
    }
}
