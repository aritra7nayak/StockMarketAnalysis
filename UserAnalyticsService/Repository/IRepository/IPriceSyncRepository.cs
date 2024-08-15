using UserAnalyticsService.DTOs;

namespace UserAnalyticsService.Repository.IRepository
{
    public interface IPriceSyncRepository
    {
        Task StorePricesAsync(List<PriceData> securities);

    }
}
