using UserAnalyticsService.DTOs;
using UserAnalyticsService.Models;

namespace UserAnalyticsService.Service.IService
{
    public interface ISyncProcess
    {
        Task SyncPricesAsync();
        Task SyncSecuritiesAsync(SecuritySyncRun securitySyncRun);

    }
}
