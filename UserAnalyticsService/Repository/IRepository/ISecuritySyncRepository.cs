using UserAnalyticsService.DTOs;
using UserAnalyticsService.Models;

namespace UserAnalyticsService.Repository.IRepository
{
    public interface ISecuritySyncRepository
    {
        Task<SecuritySyncRun> StoreSecuritiesAsync(List<SecurityData> securities);

    }
}
