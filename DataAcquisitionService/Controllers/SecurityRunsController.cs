using DataAcquisitionService.Dtos;
using DataAcquisitionService.Models;
using DataAcquisitionService.Services.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DataAcquisitionService.Controllers
{
    [Route("api/DataAcquisition/SecurityRuns")]
    [ApiController]
    [Authorize]
    public class SecurityRunsController : ControllerBase
    {
        private readonly ISecurityRunService _securityRunService;
        private ResponseDto _response;

        public SecurityRunsController(ISecurityRunService securityService)
        {
            _securityRunService = securityService;
            _response = new ResponseDto();
        }

        [HttpGet]
        [Route("GetAllSecurityRuns")]
        // GET: SecurityController
        public async Task<ResponseDto> GetAllSecurityRuns()
        {
            try
            {

                IEnumerable<SecurityRun> objList = await _securityRunService.GetAllSecurityRunsAsync();
                IEnumerable<SecurityRunDto> securityRunDtos = objList.Select(s => new SecurityRunDto
                {
                    Id = s.Id,
                    Date = s.Date,
                    SourceType = s.SourceType,
                    InsertType = s.InsertType,
                    ErrorMessage = s.ErrorMessage,
                    ProcessType = s.ProcessType,
                    CreatedBy = s.CreatedBy,
                    ModifiedBy = s.ModifiedBy,
                    RowsAdded = s.RowsAdded,
                    RowsDeleted = s.RowsDeleted,
                    RowsUpdated = s.RowsUpdated,

                });

                _response.Result = securityRunDtos;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpPost("Create")]
        public async Task<ResponseDto> Create([FromBody] SecurityRunDto securityRunDto)
        {
            SecurityRun securityRun = new();

            securityRun.InsertType = securityRunDto.InsertType;
            securityRun.ProcessType = securityRunDto.ProcessType;
            securityRun.SourceType = securityRunDto.SourceType;
            securityRun.FileStream = securityRunDto.FileStream;
            securityRun.FilePath = securityRunDto.FilePath;
            try
            {
                await _securityRunService.AddSecurityRunAsync(securityRun);


                _response.Result = securityRun;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpPost("Delete/{id}")]
        public async Task<ResponseDto> Delete(int id)
        {
            try
            {
                var security = await _securityRunService.GetSecurityRunByIdAsync(id);
                if (security != null)
                {
                    await _securityRunService.DeleteSecurityRunAsync(id);
                }

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
