using StockAnalysis.Web.Models;
using StockAnalysis.Web.Service.IService;
using StockAnalysis.Web.Utility;

namespace StockAnalysis.Web.Service
{
    public class CorporateActionTypeRunService : ICorporateActionTypeRunService
    {
        private readonly IBaseService _baseService;

        public CorporateActionTypeRunService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<ResponseDto?> AddCorporateActionTypeRunAsync(CorporateActionTypeRunDto corporateActionTypeRunDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = Utility.SD.ApiType.POST,
                Data = corporateActionTypeRunDto,
                Url = SD.DataAcquisition + "/api/DataAcquisition/CorporateActionTypeRuns/Create"
            });
        }

        public async Task<ResponseDto?> DeleteCorporateActionTypeAsync(int id)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = Utility.SD.ApiType.POST,
                Url = SD.DataAcquisition + "/api/DataAcquisition/CorporateActionTypeRuns/Delete/" + id
            });
        }

        public async Task<ResponseDto?> GetAllCorporateActionTypeRunsAsync()
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = Utility.SD.ApiType.GET,
                Url = SD.DataAcquisition + "/api/DataAcquisition/CorporateActionTypeRuns/GetAllCorporateActionTypeRuns"
            });
        }

        public async Task<ResponseDto?> GetCorporateActionTypeRunByIdAsync(int id)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = Utility.SD.ApiType.GET,
                Url = SD.DataAcquisition + "/api/DataAcquisition/CorporateActionTypeRuns/GetCorporateActionTypeRunById/" + id
            });
        }

        public async Task<ResponseDto?> UpdateCorporateActionTypeRunAsync(CorporateActionTypeRunDto corporateActionTypeRunDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = Utility.SD.ApiType.POST,
                Data = corporateActionTypeRunDto,
                Url = SD.DataAcquisition + "/api/DataAcquisition/CorporateActionTypeRuns/Edit"
            });
        }
    }
}
