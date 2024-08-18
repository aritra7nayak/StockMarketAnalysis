using StockAnalysis.Web.Models;

namespace StockAnalysis.Web.Service.IService
{
    public interface IPriceSyncService
    {
        Task<ResponseDto?> GetAllPricesAsync();
        Task<ResponseDto?> AddPriceAsync(PriceSyncProcessRuns priceSyncProcessRuns);
    }
}
