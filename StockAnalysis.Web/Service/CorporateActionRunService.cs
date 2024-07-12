using StockAnalysis.Web.Models;
using StockAnalysis.Web.Service.IService;
using StockAnalysis.Web.Utility;

namespace StockAnalysis.Web.Service
{
    public class CorporateActionRunService : ICorporateActionRunService
    {
        private readonly IBaseService _baseService;

        public CorporateActionRunService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<ResponseDto?> AddCorporateActionRunAsync(CorporateActionRunDto corporateActionRunDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = Utility.SD.ApiType.POST,
                Data = corporateActionRunDto,
                Url = SD.DataAcquisition + "/api/DataAcquisition/CorporateActionRuns/Create"
            });
        }

        public async Task<ResponseDto?> DeleteCorporateActionAsync(int id)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = Utility.SD.ApiType.POST,
                Url = SD.DataAcquisition + "/api/DataAcquisition/CorporateActionRuns/Delete/" + id
            });
        }

        public async Task<ResponseDto?> GetAllCorporateActionRunsAsync()
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = Utility.SD.ApiType.GET,
                Url = SD.DataAcquisition + "/api/DataAcquisition/CorporateActionRuns/GetAllCorporateActionRuns"
            });
        }

        public async Task<ResponseDto?> GetCorporateActionRunByIdAsync(int id)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = Utility.SD.ApiType.GET,
                Url = SD.DataAcquisition + "/api/DataAcquisition/CorporateActionRuns/GetCorporateActionRunById/" + id
            });
        }

        public async Task<ResponseDto?> UpdateCorporateActionRunAsync(CorporateActionRunDto corporateActionRunDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = Utility.SD.ApiType.POST,
                Data = corporateActionRunDto,
                Url = SD.DataAcquisition + "/api/DataAcquisition/CorporateActionRuns/Edit"
            });
        }
    }
}

