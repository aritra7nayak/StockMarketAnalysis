using UserAnalyticsService.Models;

namespace UserAnalyticsService.Repository.IRepository
{
    public interface ILatestSyncProcessDetailRepository : IRepository<LatestSyncProcessDetail>
    {
        public Task<DateTime?> GetLatestSyncDate(SyncProcessTypeEnum syncProcessType);
        public Task StoreLatestSyncProcessDetailAsync(SyncProcessTypeEnum syncProcessType, DateTime dateTime);
    }
}
