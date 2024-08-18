using UserAnalyticsService.DTOs;
using UserAnalyticsService.Models;

namespace UserAnalyticsService.Repository.IRepository
{
    public interface IPriceSyncRepository
    {
        Task<PriceSyncRun> StorePricesAsync(List<PriceData> securities);

    }
}
