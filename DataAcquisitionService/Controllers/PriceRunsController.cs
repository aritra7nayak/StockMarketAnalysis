using DataAcquisitionService.Dtos;
using DataAcquisitionService.Models;
using DataAcquisitionService.Services.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DataAcquisitionService.Controllers
{
    [Route("api/DataAcquisition/PriceRuns")]
    [ApiController]
    [Authorize]
    public class PriceRunsController : ControllerBase
    {
        private readonly IPriceRunService _priceRunService;
        private ResponseDto _response;

        public PriceRunsController(IPriceRunService priceService)
        {
            _priceRunService = priceService;
            _response = new ResponseDto();
        }

        [HttpGet]
        [Route("GetAllPriceRuns")]
        // GET: PriceController
        public async Task<ResponseDto> GetAllPriceRuns()
        {
            try
            {

                IEnumerable<PriceRun> objList = await _priceRunService.GetAllPriceRunsAsync();
                IEnumerable<PriceRunDto> priceRunDtos = objList.Select(s => new PriceRunDto
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

                _response.Result = priceRunDtos;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpPost("Create")]
        public async Task<ResponseDto> Create([FromBody] PriceRunDto priceRunDto)
        {
            PriceRun priceRun = new();
            priceRun.Date = priceRunDto.Date;
            priceRun.InsertType = priceRunDto.InsertType;
            priceRun.ProcessType = priceRunDto.ProcessType;
            priceRun.SourceType = priceRunDto.SourceType;
            priceRun.FileStream = priceRunDto.FileStream;
            priceRun.FilePath = priceRunDto.FilePath;
            try
            {
                await _priceRunService.AddPriceRunAsync(priceRun);


                _response.Result = priceRun;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpGet]
        [Route("GetPriceRunById/{id}")]
        // GET: PriceController
        public async Task<ResponseDto> GetPriceRunById(int id)
        {
            try
            {
                PriceRun objList = await _priceRunService.GetPriceRunByIdAsync(id);
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
                var price = await _priceRunService.GetPriceRunByIdAsync(id);
                if (price != null)
                {
                    await _priceRunService.DeletePriceRunAsync(id);
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
