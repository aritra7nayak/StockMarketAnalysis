using DataAcquisitionService.Dtos;
using DataAcquisitionService.Models;
using DataAcquisitionService.Services.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DataAcquisitionService.Controllers
{
    [Route("api/DataAcquisition/CorporateActions")]
    [ApiController]
    [Authorize]
    public class CorporateActionsController : ControllerBase
    {
        private readonly ICorporateActionService _corporateActionService;
        private ResponseDto _response;

        public CorporateActionsController(ICorporateActionService corporateActionService)
        {
            _corporateActionService = corporateActionService;
            _response = new ResponseDto();
        }

        [HttpGet]
        [Route("GetAllCorporateActions")]
        // GET: CorporateActionController
        public async Task<ResponseDto> GetAllCorporateActions()
        {
            try
            {
                IEnumerable<CorporateAction> objList = await _corporateActionService.GetAllCorporateActionsAsync();
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
        [Route("GetCorporateActionsbyNameorSymbolAsync/{name}")]
        // GET: CorporateActionController
        public async Task<ResponseDto> GetCorporateActionsbyNameorSymbolAsync(string name)
        {
            try
            {
                IEnumerable<CorporateAction> objList = await _corporateActionService.GetFilteredCorporateActionAsync(name);
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
        [Route("GetCorporateActionById/{id}")]
        // GET: CorporateActionController
        public async Task<ResponseDto> GetCorporateActionById(int id)
        {
            try
            {
                CorporateAction objList = await _corporateActionService.GetCorporateActionByIdAsync(id);
                _response.Result = objList;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        // GET: CorporateActionController/Details/5
        //public async Task<ActionResult> Details(int id)
        //{
        //    var corporateAction = await _corporateActionService.GetCorporateActionByIdAsync(id);
        //    if (corporateAction == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(corporateAction);
        //}

        // POST: CorporateActionController/Create
        [HttpPost("Create")]
        public async Task<ResponseDto> Create([FromBody] CorporateActionDTO corporateActionDto)
        {
            CorporateAction corporateAction = new();

            corporateAction.Date = corporateActionDto.Date;
            corporateAction.SecurityID = corporateActionDto.SecurityID;
            corporateAction.Purpose = corporateActionDto.Purpose;
            corporateAction.CorporateActionTypeID = corporateActionDto.CorporateActionTypeID;
            corporateAction.FaceValue = corporateActionDto.FaceValue;
            corporateAction.Record_Date = corporateActionDto.Record_Date;
            corporateAction.Book_Closure_Start_Date = corporateActionDto.Book_Closure_Start_Date;
            corporateAction.Book_Closure_End_Date = corporateActionDto.Book_Closure_End_Date;
            corporateAction.CreatedBy = User.Identity.Name;
            corporateAction.ModifiedBy = User.Identity.Name;
            try
            {
                await _corporateActionService.AddCorporateActionAsync(corporateAction);


                _response.Result = corporateAction;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        // POST: CorporateActionController/Edit/5
        [HttpPost("Edit")]
        public async Task<ResponseDto> Edit(CorporateActionDTO corporateActionDto)
        {
            CorporateAction corporateAction = new();


            corporateAction.Date = corporateActionDto.Date;
            corporateAction.SecurityID = corporateActionDto.SecurityID;
            corporateAction.Purpose = corporateActionDto.Purpose;
            corporateAction.CorporateActionTypeID = corporateActionDto.CorporateActionTypeID;
            corporateAction.FaceValue = corporateActionDto.FaceValue;
            corporateAction.Record_Date = corporateActionDto.Record_Date;
            corporateAction.Book_Closure_Start_Date = corporateActionDto.Book_Closure_Start_Date;
            corporateAction.Book_Closure_End_Date = corporateActionDto.Book_Closure_End_Date;
            corporateAction.ID = (int)corporateActionDto.ID;
            corporateAction.ModifiedBy = User.Identity.Name;
            corporateAction.ModifiedOn = DateTime.Now;
            try
            {
                await _corporateActionService.UpdateCorporateActionAsync(corporateAction);

                _response.Result = corporateAction;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }


        // POST: CorporateActionController/Delete/5
        [HttpPost("Delete/{id}")]
        public async Task<ResponseDto> Delete(int id)
        {
            try
            {
                var corporateAction = await _corporateActionService.GetCorporateActionByIdAsync(id);
                if (corporateAction != null)
                {
                    await _corporateActionService.DeleteCorporateActionAsync(id);
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
