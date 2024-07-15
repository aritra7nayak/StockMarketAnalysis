using StockAnalysis.Web.Models;
using StockAnalysis.Web.Service.IService;
using StockAnalysis.Web.Utility;

namespace StockAnalysis.Web.Service
{
    public class CorporateActionService:ICorporateActionService
    {
        private readonly IBaseService _baseService;

        public CorporateActionService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<ResponseDto?> AddCorporateActionAsync(CorporateAction corporateAction)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = Utility.SD.ApiType.POST,
                Data = corporateAction,
                Url = SD.DataAcquisition + "/api/DataAcquisition/CorporateActions/Create"
            });
        }

        public async Task<ResponseDto?> DeleteCorporateActionAsync(int id)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = Utility.SD.ApiType.POST,
                Url = SD.DataAcquisition + "/api/DataAcquisition/CorporateActions/Delete/" + id
            });
        }

        public async Task<ResponseDto?> GetAllCorporateActionsAsync()
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = Utility.SD.ApiType.GET,
                Url = SD.DataAcquisition + "/api/DataAcquisition/CorporateActions/GetAllCorporateActions"
            });
        }
        public async Task<ResponseDto?> GetSecuritiesbyNameorSymbolAsync(string name)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = Utility.SD.ApiType.GET,
                Url = SD.DataAcquisition + "/api/DataAcquisition/CorporateActions/GetCorporateActionsbyNameorSymbolAsync/" + name
            });
        }

        public async Task<ResponseDto?> GetFilteredCorporateActionAsync(string name, string symbol)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseDto?> GetCorporateActionByIdAsync(int id)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = Utility.SD.ApiType.GET,
                Url = SD.DataAcquisition + "/api/DataAcquisition/CorporateActions/GetCorporateActionById/" + id
            });
        }

        public async Task<ResponseDto?> UpdateCorporateActionAsync(CorporateAction corporateAction)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = Utility.SD.ApiType.POST,
                Data = corporateAction,
                Url = SD.DataAcquisition + "/api/DataAcquisition/CorporateActions/Edit"
            });
        }
    }
}
