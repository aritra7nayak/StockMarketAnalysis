using DataAcquisitionService.Dtos;
using DataAcquisitionService.Models;
using DataAcquisitionService.Services.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DataAcquisitionService.Controllers
{
    [Route("api/DataAcquisition/CorporateActionTypeRuns")]
    [ApiController]
    [Authorize]
    public class CorporateActionTypeRunsController : ControllerBase
    {
        private readonly ICorporateActionTypeRunService _corporateActionTypeRunService;
        private ResponseDto _response;

        public CorporateActionTypeRunsController(ICorporateActionTypeRunService corporateActionTypeService)
        {
            _corporateActionTypeRunService = corporateActionTypeService;
            _response = new ResponseDto();
        }

        [HttpGet]
        [Route("GetAllCorporateActionTypeRuns")]
        // GET: CorporateActionTypeController
        public async Task<ResponseDto> GetAllCorporateActionTypeRuns()
        {
            try
            {

                IEnumerable<CorporateActionTypeRun> objList = await _corporateActionTypeRunService.GetAllCorporateActionTypeRunsAsync();
                IEnumerable<CorporateActionTypeRunDto> corporateActionTypeRunDtos = objList.Select(s => new CorporateActionTypeRunDto
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

                _response.Result = corporateActionTypeRunDtos;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpPost("Create")]
        public async Task<ResponseDto> Create([FromBody] CorporateActionTypeRunDto corporateActionTypeRunDto)
        {
            CorporateActionTypeRun corporateActionTypeRun = new();
            corporateActionTypeRun.Date = corporateActionTypeRunDto.Date;
            corporateActionTypeRun.InsertType = corporateActionTypeRunDto.InsertType;
            corporateActionTypeRun.ProcessType = corporateActionTypeRunDto.ProcessType;
            corporateActionTypeRun.SourceType = corporateActionTypeRunDto.SourceType;
            corporateActionTypeRun.FileStream = corporateActionTypeRunDto.FileStream;
            corporateActionTypeRun.FilePath = corporateActionTypeRunDto.FilePath;
            try
            {
                await _corporateActionTypeRunService.AddCorporateActionTypeRunAsync(corporateActionTypeRun);


                _response.Result = corporateActionTypeRun;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpGet]
        [Route("GetCorporateActionTypeRunById/{id}")]
        // GET: CorporateActionTypeController
        public async Task<ResponseDto> GetCorporateActionTypeRunById(int id)
        {
            try
            {
                CorporateActionTypeRun objList = await _corporateActionTypeRunService.GetCorporateActionTypeRunByIdAsync(id);
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
                var corporateActionType = await _corporateActionTypeRunService.GetCorporateActionTypeRunByIdAsync(id);
                if (corporateActionType != null)
                {
                    await _corporateActionTypeRunService.DeleteCorporateActionTypeRunAsync(id);
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
