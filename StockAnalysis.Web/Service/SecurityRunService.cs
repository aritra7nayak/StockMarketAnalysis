using StockAnalysis.Web.Models;
using StockAnalysis.Web.Service.IService;
using StockAnalysis.Web.Utility;

namespace StockAnalysis.Web.Service
{
    public class SecurityRunService: ISecurityRunService
    {
        private readonly IBaseService _baseService;

        public SecurityRunService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<ResponseDto?> AddSecurityRunAsync(SecurityRunDto securityRunDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = Utility.SD.ApiType.POST,
                Data = securityRunDto,
                Url = SD.DataAcquisition + "/api/DataAcquisition/SecurityRuns/Create"
            });
        }

        public async Task<ResponseDto?> DeleteSecurityAsync(int id)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = Utility.SD.ApiType.POST,
                Url = SD.DataAcquisition + "/api/DataAcquisition/SecurityRuns/Delete/" + id
            });
        }

        public async Task<ResponseDto?> GetAllSecurityRunsAsync()
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = Utility.SD.ApiType.GET,
                Url = SD.DataAcquisition + "/api/DataAcquisition/SecurityRuns/GetAllSecurityRuns"
            });
        }

        public async Task<ResponseDto?> GetSecurityRunByIdAsync(int id)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = Utility.SD.ApiType.GET,
                Url = SD.DataAcquisition + "/api/DataAcquisition/SecurityRuns/GetSecurityRunById/" + id
            });
        }

        public async Task<ResponseDto?> UpdateSecurityRunAsync(SecurityRunDto securityRunDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = Utility.SD.ApiType.POST,
                Data = securityRunDto,
                Url = SD.DataAcquisition + "/api/DataAcquisition/SecurityRuns/Edit"
            });
        }
    }
}
