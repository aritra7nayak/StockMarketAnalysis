using DataAcquisitionService.Dtos.SyncDto;
using DataAcquisitionService.Services.IService;
using DataAcquisitionService.Utlities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace DataAcquisitionService.Controllers
{
    [Route("api/SyncProcess")]
    [ApiController]
    public class SyncProcessController : ControllerBase
    {
        private readonly ISyncService _syncService;
        private readonly TokenSettings _tokenSettings;

        public SyncProcessController(ISyncService syncService, IOptions<TokenSettings> tokenSettings)
        {
            _syncService = syncService;
            _tokenSettings = tokenSettings.Value;
        }

        [HttpPost("GetSecurities")]
        public IActionResult GetSecurities([FromBody] SyncRequestViewModel request)
        {
            // Verify token
            if (request.Token != _tokenSettings.ApiToken)
            {
                return Unauthorized(new
                {
                    Success = false,
                    Message = "Invalid token."
                });
            }

            if (ModelState.IsValid)
            {
                var response = _syncService.GetSecurityData(request.LastUpdatedDate);
                response.Success = true;
                return Ok(response);
            }

            return BadRequest(new
            {
                Success = false,
                Message = "Invalid request."
            });
        }
        [HttpPost("GetPrices")]
        public IActionResult GetPrices([FromBody] SyncRequestViewModel request)
        {
            // Verify token
            if (request.Token != _tokenSettings.ApiToken)
            {
                return Unauthorized(new
                {
                    Success = false,
                    Message = "Invalid token."
                });
            }

            if (ModelState.IsValid)
            {
                var response = _syncService.GetPriceData(request.LastUpdatedDate);
                response.Success = true;
                return Ok(response);
            }

            return BadRequest(new
            {
                Success = false,
                Message = "Invalid request."
            });
        }
    }
}
