using DataAcquisitionService.Dtos;
using DataAcquisitionService.Models;
using DataAcquisitionService.Services.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DataAcquisitionService.Controllers
{
    [Route("api/DataAcquisition/CorporateActionRuns")]
    [ApiController]
    [Authorize]
    public class CorporateActionRunsController : ControllerBase
    {
        private readonly ICorporateActionRunService _corporateActionRunService;
        private ResponseDto _response;

        public CorporateActionRunsController(ICorporateActionRunService corporateActionService)
        {
            _corporateActionRunService = corporateActionService;
            _response = new ResponseDto();
        }

        [HttpGet]
        [Route("GetAllCorporateActionRuns")]
        // GET: CorporateActionController
        public async Task<ResponseDto> GetAllCorporateActionRuns()
        {
            try
            {

                IEnumerable<CorporateActionRun> objList = await _corporateActionRunService.GetAllCorporateActionRunsAsync();
                IEnumerable<CorporateActionRunDto> corporateActionRunDtos = objList.Select(s => new CorporateActionRunDto
                {
                    Id = s.Id,
                    Date = s.Date,
                    SourceType = s.SourceType,
                    InsertType = s.InsertType,
                    ErrorMessage = s.ErrorMessage,
                    ProcessType = s.ProcessType,
                    //CreatedBy = s.CreatedBy,
                    //ModifiedBy = s.ModifiedBy,
                    RowsAdded = s.RowsAdded,
                    RowsDeleted = s.RowsDeleted,
                    RowsUpdated = s.RowsUpdated,
                    RowsTotal = s.RowsTotal

                }).OrderByDescending(o => o.Date);

                _response.Result = corporateActionRunDtos;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpPost("Create")]
        public async Task<ResponseDto> Create([FromBody] CorporateActionRunDto corporateActionRunDto)
        {
            CorporateActionRun corporateActionRun = new();
            corporateActionRun.Date = corporateActionRunDto.Date;
            corporateActionRun.InsertType = corporateActionRunDto.InsertType;
            corporateActionRun.ProcessType = corporateActionRunDto.ProcessType;
            corporateActionRun.SourceType = corporateActionRunDto.SourceType;
            corporateActionRun.FileStream = corporateActionRunDto.FileStream;
            corporateActionRun.FilePath = corporateActionRunDto.FilePath;
            try
            {
                await _corporateActionRunService.AddCorporateActionRunAsync(corporateActionRun);


                _response.Result = corporateActionRun;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpGet]
        [Route("GetCorporateActionRunById/{id}")]
        // GET: CorporateActionController
        public async Task<ResponseDto> GetCorporateActionRunById(int id)
        {
            try
            {
                CorporateActionRun objList = await _corporateActionRunService.GetCorporateActionRunByIdAsync(id);
                _response.Result = objList;
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
                var corporateAction = await _corporateActionRunService.GetCorporateActionRunByIdAsync(id);
                if (corporateAction != null)
                {
                    await _corporateActionRunService.DeleteCorporateActionRunAsync(id);
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
