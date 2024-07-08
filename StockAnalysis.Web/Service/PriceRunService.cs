using StockAnalysis.Web.Models;
using StockAnalysis.Web.Service.IService;
using StockAnalysis.Web.Utility;

namespace StockAnalysis.Web.Service
{
    public class PriceRunService : IPriceRunService
    {
        private readonly IBaseService _baseService;

        public PriceRunService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<ResponseDto?> AddPriceRunAsync(PriceRunDto priceRunDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = Utility.SD.ApiType.POST,
                Data = priceRunDto,
                Url = SD.DataAcquisition + "/api/DataAcquisition/PriceRuns/Create"
            });
        }

        public async Task<ResponseDto?> DeletePriceAsync(int id)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = Utility.SD.ApiType.POST,
                Url = SD.DataAcquisition + "/api/DataAcquisition/PriceRuns/Delete/" + id
            });
        }

        public async Task<ResponseDto?> GetAllPriceRunsAsync()
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = Utility.SD.ApiType.GET,
                Url = SD.DataAcquisition + "/api/DataAcquisition/PriceRuns/GetAllPriceRuns"
            });
        }

        public async Task<ResponseDto?> GetPriceRunByIdAsync(int id)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = Utility.SD.ApiType.GET,
                Url = SD.DataAcquisition + "/api/DataAcquisition/PriceRuns/GetPriceRunById/" + id
            });
        }

        public async Task<ResponseDto?> UpdatePriceRunAsync(PriceRunDto priceRunDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = Utility.SD.ApiType.POST,
                Data = priceRunDto,
                Url = SD.DataAcquisition + "/api/DataAcquisition/PriceRuns/Edit"
            });
        }
    }
}
