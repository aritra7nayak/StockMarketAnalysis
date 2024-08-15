using UserAnalyticsService.DTOs;

namespace UserAnalyticsService.Service.IService
{
    public interface ISyncProcess
    {
        Task SyncPricesAsync(List<PriceData> securities);
        Task SyncSecuritiesAsync();

    }
}
