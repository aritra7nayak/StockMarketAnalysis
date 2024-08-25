using UserAnalyticsService.DTOs;
using UserAnalyticsService.Models;

namespace UserAnalyticsService.Repository.IRepository
{
    public interface IPriceSyncRepository
    {
        Task<PriceSyncRun> StorePricesAsync(List<PriceData> securities);
        Task<List<SecurityAutoCompleteDto>> GetSecuritiesAutocompleteAsync(string securityNamePart);

    }
}
