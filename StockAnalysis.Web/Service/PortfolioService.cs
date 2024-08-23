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

        public async Task<ResponseDto?> AddPortfolioAsync(Portfolio portfolio)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = Utility.SD.ApiType.POST,
                Data = portfolio,
                Url = SD.UserAnalytics + "/api/Portfolio/AddPortfolio"
            });
        }

        public Task<ResponseDto?> DeletePortfolioAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseDto?> GetUserPortfoliosAsync()
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = Utility.SD.ApiType.GET,
                Url = SD.UserAnalytics + "/api/Portfolio/GetPortfoliosByOwner"
            });
        }

        public async Task<ResponseDto?> GetPortfolioByIdAsync(Guid id)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = Utility.SD.ApiType.GET,
                Url = SD.UserAnalytics + "/api/Portfolio/GetPortfolioById/" + id,
            });
        }

        public async Task<ResponseDto?> GetSecurityAutoComplete(string name)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = Utility.SD.ApiType.GET,
                Url = SD.UserAnalytics + "/api/Portfolio/GetSecurityAutoComplete/"+name
            });
        }

        public async Task<ResponseDto?> UpdatePortfolioAsync(Portfolio portfolio)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = Utility.SD.ApiType.POST,
                Data = portfolio,
                Url = SD.UserAnalytics + "/api/Portfolio/Edit"
            });
        }
    }
}
