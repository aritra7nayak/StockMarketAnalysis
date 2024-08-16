using MongoDB.Driver;
using UserAnalyticsService.Models;

namespace UserAnalyticsService.Repository.IRepository
{
    public class SecuritySyncRunRepository : GenericRepository<SecuritySyncRun>, ISecuritySyncRunRepository
    {
        private readonly IMongoDatabase _database;

        public SecuritySyncRunRepository(IMongoDatabase database)
            : base(database, "SecuritySyncRuns") // "SecuritySyncRuns" is the collection name in MongoDB
        {
            _database = database;
        }

        public IMongoCollection<SecuritySyncRun> SecuritySyncRuns => _collection;
    }
}
