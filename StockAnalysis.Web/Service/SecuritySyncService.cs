using StockAnalysis.Web.Models;
using StockAnalysis.Web.Service.IService;
using StockAnalysis.Web.Utility;

namespace StockAnalysis.Web.Service
{
    public class SecuritySyncService : ISecuritySyncService
    {
        private readonly IBaseService _baseService;

        public SecuritySyncService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<ResponseDto?> AddSecurityAsync(SecuritySyncProcessRuns securitySyncProcessRuns)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = Utility.SD.ApiType.POST,
                Data = securitySyncProcessRuns,
                Url = SD.UserAnalytics + "/api/SecuritySyncProcess/Create"
            });
        }

        public async Task<ResponseDto?> GetAllSecuritysAsync()
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = Utility.SD.ApiType.GET,
                Url = SD.UserAnalytics + "/api/SecuritySyncProcess/GetAllSecuritySyncRuns"
            });
        }
    }
}
