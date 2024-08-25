using Microsoft.AspNetCore.Mvc;
using UserAnalyticsService.DTOs;
using UserAnalyticsService.Models;
using UserAnalyticsService.Service.IService;

namespace UserAnalyticsService.Controllers
{
    [Route("api/PriceSyncProcess/")]
    [ApiController]
    public class PriceSyncProcessController : ControllerBase
    {
        private readonly ISyncProcess _syncProcess;
        private ResponseDto _response;


        public PriceSyncProcessController(ISyncProcess syncProcess)
        {
            _syncProcess = syncProcess;
            _response = new ResponseDto();

        }

        [HttpPost("Create")]
        public async Task<ResponseDto> Create()
        {
            PriceSyncRun priceSyncRun = new PriceSyncRun();
            try
            {
                await _syncProcess.SyncPricesAsync(priceSyncRun);


                _response.Result = priceSyncRun;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpGet]
        [Route("GetAllPriceSyncRuns")]
        // GET: PriceController
        public async Task<ResponseDto> GetAllPriceSyncRuns()
        {
            try
            {

                IEnumerable<PriceSyncRun> objList = await _syncProcess.GetAllPriceSyncRuns();
                IEnumerable<PriceSyncRun> priceSyncRunDtos = objList.Select(s => new PriceSyncRun
                {
                    Id = s.Id,
                    ProcessUpdateTillDate = s.ProcessUpdateTillDate,
                    UpdateTillDate = s.UpdateTillDate,
                    CreatedOn = s.CreatedOn,
                    IsSuccess = s.IsSuccess,
                    ErrorMessage = s.ErrorMessage,
                    ModifiedOn = s.ModifiedOn,
                    CreatedBy = s.CreatedBy,
                    ModifiedBy = s.ModifiedBy,
                    RowsAdded = s.RowsAdded,
                    RowsUpdated = s.RowsUpdated,
                    RowsTotal = s.RowsTotal

                }).OrderByDescending(o => o.Id);

                _response.Result = priceSyncRunDtos;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }
    }
}
