using Microsoft.AspNetCore.Mvc;
using UserAnalyticsService.DTOs;
using UserAnalyticsService.Models;
using UserAnalyticsService.Service.IService;

namespace UserAnalyticsService.Controllers
{
    [Route("api/DataAcquisition/PriceRuns")]
    [ApiController]
    public class SecuritySyncProcessController : ControllerBase
    {
        private readonly ISyncProcess _syncProcess;
        private ResponseDto _response;


        public SecuritySyncProcessController(ISyncProcess syncProcess)
        {
            _syncProcess = syncProcess;
            _response = new ResponseDto();

        }

        [HttpPost("Create")]
        public async Task<ResponseDto> Create()
        {
            SecuritySyncRun securitySyncRun = new SecuritySyncRun();
            try
            {
                await _syncProcess.SyncSecuritiesAsync(securitySyncRun);


                _response.Result = securitySyncRun;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        //[HttpGet]
        //[Route("GetAllPriceRuns")]
        //// GET: PriceController
        //public async Task<ResponseDto> GetAllPriceRuns()
        //{
        //    try
        //    {

        //        IEnumerable<PriceRun> objList = await _priceRunService.GetAllPriceRunsAsync();
        //        IEnumerable<PriceRunDto> priceRunDtos = objList.Select(s => new PriceRunDto
        //        {
        //            Id = s.Id,
        //            Date = s.Date,
        //            SourceType = s.SourceType,
        //            InsertType = s.InsertType,
        //            ErrorMessage = s.ErrorMessage,
        //            ProcessType = s.ProcessType,
        //            //CreatedBy = s.CreatedBy,
        //            //ModifiedBy = s.ModifiedBy,
        //            RowsAdded = s.RowsAdded,
        //            RowsDeleted = s.RowsDeleted,
        //            RowsUpdated = s.RowsUpdated,
        //            RowsTotal = s.RowsTotal

        //        }).OrderByDescending(o => o.Date);

        //        _response.Result = priceRunDtos;
        //    }
        //    catch (Exception ex)
        //    {
        //        _response.IsSuccess = false;
        //        _response.Message = ex.Message;
        //    }
        //    return _response;
        //}
    }
}
