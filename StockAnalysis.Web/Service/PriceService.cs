using StockAnalysis.Web.Models;
using StockAnalysis.Web.Service.IService;
using StockAnalysis.Web.Utility;

namespace StockAnalysis.Web.Service
{
    public class PriceService : IPriceService
    {
        private readonly IBaseService _baseService;

        public PriceService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<ResponseDto?> AddPriceAsync(Price price)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = Utility.SD.ApiType.POST,
                Data = price,
                Url = SD.DataAcquisition + "/api/DataAcquisition/Price/Create"
            });
        }

        public async Task<ResponseDto?> DeletePriceAsync(int id)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = Utility.SD.ApiType.POST,
                Url = SD.DataAcquisition + "/api/DataAcquisition/Price/Delete/" + id
            });
        }

        public async Task<ResponseDto?> GetAllPricesAsync()
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = Utility.SD.ApiType.GET,
                Url = SD.DataAcquisition + "/api/DataAcquisition/Price/GetAllPrices"
            });
        }
        public async Task<ResponseDto?> GetSecuritiesbyNameorSymbolAsync(string name)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = Utility.SD.ApiType.GET,
                Url = SD.DataAcquisition + "/api/DataAcquisition/Price/GetSecuritiesbyNameorSymbolAsync/" + name
            });
        }

        public async Task<ResponseDto?> GetFilteredPriceAsync(string name, string symbol)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseDto?> GetPriceByIdAsync(int id)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = Utility.SD.ApiType.GET,
                Url = SD.DataAcquisition + "/api/DataAcquisition/Price/GetPriceById/" + id
            });
        }

        public async Task<ResponseDto?> UpdatePriceAsync(Price price)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = Utility.SD.ApiType.POST,
                Data = price,
                Url = SD.DataAcquisition + "/api/DataAcquisition/Price/Edit"
            });
        }
    }
}
