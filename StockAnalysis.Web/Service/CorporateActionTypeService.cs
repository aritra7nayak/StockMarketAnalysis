using StockAnalysis.Web.Models;
using StockAnalysis.Web.Service.IService;
using StockAnalysis.Web.Utility;

namespace StockAnalysis.Web.Service
{
    public class CorporateActionTypeService : ICorporateActionTypeService
    {
        private readonly IBaseService _baseService;

        public CorporateActionTypeService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<ResponseDto?> AddCorporateActionTypeAsync(CorporateActionType corporateActionType)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = Utility.SD.ApiType.POST,
                Data = corporateActionType,
                Url = SD.DataAcquisition + "/api/DataAcquisition/CorporateActionTypes/Create"
            });
        }

        public async Task<ResponseDto?> DeleteCorporateActionTypeAsync(int id)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = Utility.SD.ApiType.POST,
                Url = SD.DataAcquisition + "/api/DataAcquisition/CorporateActionTypes/Delete/" + id
            });
        }

        public async Task<ResponseDto?> GetAllCorporateActionTypesAsync()
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = Utility.SD.ApiType.GET,
                Url = SD.DataAcquisition + "/api/DataAcquisition/CorporateActionTypes/GetAllCorporateActionTypes"
            });
        }
        public async Task<ResponseDto?> GetCorporateActionTypesbyNameorSymbolAsync(string name)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = Utility.SD.ApiType.GET,
                Url = SD.DataAcquisition + "/api/DataAcquisition/CorporateActionTypes/GetCorporateActionTypesbyNameorSymbolAsync/" + name
            });
        }

        public async Task<ResponseDto?> GetFilteredCorporateActionTypeAsync(string name, string symbol)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseDto?> GetCorporateActionTypeByIdAsync(int id)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = Utility.SD.ApiType.GET,
                Url = SD.DataAcquisition + "/api/DataAcquisition/CorporateActionType/GetCorporateActionTypeById/" + id
            });
        }

        public async Task<ResponseDto?> UpdateCorporateActionTypeAsync(CorporateActionType corporateActionType)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = Utility.SD.ApiType.POST,
                Data = corporateActionType,
                Url = SD.DataAcquisition + "/api/DataAcquisition/CorporateActionType/Edit"
            });
        }
    }
}
