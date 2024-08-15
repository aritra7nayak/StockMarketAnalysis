using UserAnalyticsService.DTOs;

namespace UserAnalyticsService.Repository.IRepository
{
    public interface ISecuritySyncRepository
    {
        Task StoreSecuritiesAsync(List<SecurityData> securities);

    }
}
