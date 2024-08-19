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

        public async Task<DateTime?> GetLatestSyncDate(SyncProcessTypeEnum syncProcessType)
        {
            var filter = Builders<LatestSyncProcessDetail>.Filter.Eq(s => s.SyncProcessType, syncProcessType);
            var latestSyncDetails = await _collection.Find(filter).FirstOrDefaultAsync();
            if (latestSyncDetails != null)
            {
                return latestSyncDetails.ProcessUpdateTillDate;
            }
            return null;
        }
        public async Task StoreLatestSyncProcessDetailAsync(SyncProcessTypeEnum syncProcessType, DateTime updateTillDate)
        {
            var filter = Builders<LatestSyncProcessDetail>.Filter.Eq(s => s.SyncProcessType, syncProcessType);
            var latestSyncDetails = await _collection.Find(filter).FirstOrDefaultAsync();
            if (latestSyncDetails != null)
            {
                // If it exists, update the fields
                var update = Builders<LatestSyncProcessDetail>.Update
                            .Set(nameof(LatestSyncProcessDetail.ProcessUpdateTillDate), updateTillDate)
                            ;

                await _collection.UpdateOneAsync(filter, update);
            }
            else
            {
                // If it doesn't exist, insert a new document
                var newLatestSyncProcessDetail = new LatestSyncProcessDetail
                {
                    ProcessUpdateTillDate = updateTillDate,
                    SyncProcessType = syncProcessType
                };

                await _collection.InsertOneAsync(newLatestSyncProcessDetail);
            }


        }
    }
}