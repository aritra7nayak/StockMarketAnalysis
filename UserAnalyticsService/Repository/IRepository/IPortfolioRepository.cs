using UserAnalyticsService.DTOs;
using UserAnalyticsService.Models;

namespace UserAnalyticsService.Repository.IRepository
{
    public interface IPortfolioRepository : IRepository<Portfolio>
    {
        Task UpdatePresentPricesAsync(List<PriceData> priceDataList);
    }
}
