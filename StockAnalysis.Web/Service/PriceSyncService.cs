using StockAnalysis.Web.Models;
using StockAnalysis.Web.Service.IService;
using StockAnalysis.Web.Utility;

namespace StockAnalysis.Web.Service
{
    public class PriceSyncService:IPriceSyncService
    {
        private readonly IBaseService _baseService;

        public PriceSyncService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<ResponseDto?> AddPriceAsync(PriceSyncProcessRuns priceSyncProcessRuns)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = Utility.SD.ApiType.POST,
                Data = priceSyncProcessRuns,
                Url = SD.UserAnalytics + "/api/PriceSyncProcess/Create"
            });
        }

        public async Task<ResponseDto?> GetAllPricesAsync()
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = Utility.SD.ApiType.GET,
                Url = SD.UserAnalytics + "/api/PriceSyncProcess/GetAllPriceSyncRuns"
            });
        }
    }
}
