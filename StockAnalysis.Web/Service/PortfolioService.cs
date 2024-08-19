using StockAnalysis.Web.Models;
using StockAnalysis.Web.Service.IService;
using StockAnalysis.Web.Utility;

namespace StockAnalysis.Web.Service
{
    public class PortfolioService: IPortfolioService
    {
        private readonly IBaseService _baseService;

        public PortfolioService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<ResponseDto?> GetSecurityAutoComplete(string name)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = Utility.SD.ApiType.GET,
                Url = SD.UserAnalytics + "/api/Portfolio/GetSecurityAutoComplete/"+name
            });
        }
    }
}
