using Microsoft.AspNetCore.Mvc;
using UserAnalyticsService.DTOs;
using UserAnalyticsService.Models;
using UserAnalyticsService.Service.IService;

namespace UserAnalyticsService.Controllers
{
    [Route("api/SecuritySyncProcess/")]
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

        [HttpGet]
        [Route("GetAllSecuritySyncRuns")]
        // GET: PriceController
        public async Task<ResponseDto> GetAllSecuritySyncRuns()
        {
            try
            {

                IEnumerable<SecuritySyncRun> objList = await _syncProcess.GetAllSecuritySyncRuns();
                IEnumerable<SecuritySyncRun> securitySyncRunDtos = objList.Select(s => new SecuritySyncRun
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

                _response.Result = securitySyncRunDtos;
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
