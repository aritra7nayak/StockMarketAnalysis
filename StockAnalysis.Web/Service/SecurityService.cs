using StockAnalysis.Web.Models;
using StockAnalysis.Web.Service.IService;
using StockAnalysis.Web.Utility;

namespace StockAnalysis.Web.Service
{
    public class SecurityService : ISecurityService
    {
        private readonly IBaseService _baseService;

        public  SecurityService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<ResponseDto?> AddSecurityAsync(Security security)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = Utility.SD.ApiType.POST,
                Data = security,
                Url = SD.DataAcquisition + "/api/DataAcquisition/Security/Create"
            });
        }

        public async Task<ResponseDto?> DeleteSecurityAsync(int id)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = Utility.SD.ApiType.POST,
                Url = SD.DataAcquisition + "/api/DataAcquisition/Security/Delete/" + id
            });
        }

        public async Task<ResponseDto?> GetAllSecuritysAsync()
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = Utility.SD.ApiType.GET,
                Url = SD.DataAcquisition + "/api/DataAcquisition/Security/GetAllSecurities"
            });
        }
        public async Task<ResponseDto?> GetSecuritiesbyNameorSymbolAsync(string name)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = Utility.SD.ApiType.GET,
                Url = SD.DataAcquisition + "/api/DataAcquisition/Security/GetSecuritiesbyNameorSymbolAsync/" + name
            }) ;
        }

        public async Task<ResponseDto?> GetFilteredSecurityAsync(string name, string symbol)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseDto?> GetSecurityByIdAsync(int id)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = Utility.SD.ApiType.GET,
                Url = SD.DataAcquisition + "/api/DataAcquisition/Security/GetSecurityById/" + id
            });
        }

        public async Task<ResponseDto?> UpdateSecurityAsync(Security security)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = Utility.SD.ApiType.POST,
                Data = security,
                Url = SD.DataAcquisition + "/api/DataAcquisition/Security/Edit"
            });
        }
    }
}
