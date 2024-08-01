using DataAcquisitionService.Dtos;
using DataAcquisitionService.Models;
using DataAcquisitionService.Services.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DataAcquisitionService.Controllers
{
    [Route("api/DataAcquisition/CorporateActionTypes")]
    [ApiController]
  //  [Authorize]
    public class CorporateActionTypesController : ControllerBase
    {
        private readonly ICorporateActionTypeService _corporateActionTypeService;
        private ResponseDto _response;

        public CorporateActionTypesController(ICorporateActionTypeService corporateActionTypeService)
        {
            _corporateActionTypeService = corporateActionTypeService;
            _response = new ResponseDto();
        }

        [HttpGet]
        [Route("GetAllCorporateActionTypes")]
        // GET: CorporateActionTypeController
        public async Task<ResponseDto> GetAllCorporateActionTypes()
        {
            try
            {
                IEnumerable<CorporateActionType> objList = await _corporateActionTypeService.GetAllCorporateActionTypesAsync();
                _response.Result = objList;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpGet]
        [Route("GetCorporateActionTypesbyNameorSymbolAsync/{name}")]
        // GET: CorporateActionTypeController
        public async Task<ResponseDto> GetCorporateActionTypesbyNameorSymbolAsync(string name)
        {
            try
            {
                IEnumerable<CorporateActionType> objList = await _corporateActionTypeService.GetFilteredCorporateActionTypeAsync(name);
                _response.Result = objList;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpGet]
        [Route("GetCorporateActionTypeById/{id}")]
        // GET: CorporateActionTypeController
        public async Task<ResponseDto> GetCorporateActionTypeById(int id)
        {
            try
            {
                CorporateActionType objList = await _corporateActionTypeService.GetCorporateActionTypeByIdAsync(id);
                _response.Result = objList;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        // GET: CorporateActionTypeController/Details/5
        //public async Task<ActionResult> Details(int id)
        //{
        //    var corporateActionType = await _corporateActionTypeService.GetCorporateActionTypeByIdAsync(id);
        //    if (corporateActionType == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(corporateActionType);
        //}

        // POST: CorporateActionTypeController/Create
        [HttpPost("Create")]
        public async Task<ResponseDto> Create([FromBody] CorporateActionTypeDTO corporateActionTypeDto)
        {
            CorporateActionType corporateActionType = new();

            corporateActionType.Name = corporateActionTypeDto.Name;
            corporateActionType.CreatedBy = User.Identity.Name;
            corporateActionType.ModifiedBy = User.Identity.Name;
            try
            {
                await _corporateActionTypeService.AddCorporateActionTypeAsync(corporateActionType);


                _response.Result = corporateActionType;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        // POST: CorporateActionTypeController/Edit/5
        [HttpPost("Edit")]
        public async Task<ResponseDto> Edit(CorporateActionTypeDTO corporateActionTypeDto)
        {
            CorporateActionType corporateActionType = new();

            corporateActionType.Name = corporateActionTypeDto.Name;
            corporateActionType.ID = (int)corporateActionTypeDto.ID;
            corporateActionType.ModifiedBy = User.Identity.Name;
            corporateActionType.ModifiedOn = DateTime.Now;
            try
            {
                await _corporateActionTypeService.UpdateCorporateActionTypeAsync(corporateActionType);

                _response.Result = corporateActionType;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }


        // POST: CorporateActionTypeController/Delete/5
        [HttpPost("Delete/{id}")]
        public async Task<ResponseDto> Delete(int id)
        {
            try
            {
                var corporateActionType = await _corporateActionTypeService.GetCorporateActionTypeByIdAsync(id);
                if (corporateActionType != null)
                {
                    await _corporateActionTypeService.DeleteCorporateActionTypeAsync(id);
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
