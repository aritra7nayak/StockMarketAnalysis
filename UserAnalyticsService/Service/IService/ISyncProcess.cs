using UserAnalyticsService.DTOs;
using UserAnalyticsService.Models;

namespace UserAnalyticsService.Service.IService
{
    public interface ISyncProcess
    {
        Task SyncPricesAsync(PriceSyncRun priceSyncRun);
        Task SyncSecuritiesAsync(SecuritySyncRun securitySyncRun);
        Task<IEnumerable<SecuritySyncRun>> GetAllSecuritySyncRuns();
        Task<IEnumerable<PriceSyncRun>> GetAllPriceSyncRuns();

    }
}
