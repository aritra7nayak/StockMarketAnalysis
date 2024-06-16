using DataAcquisitionService.Dtos;
using DataAcquisitionService.Models;
using DataAcquisitionService.Services.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace DataAcquisitionService.Controllers
{
    [Route("api/DataAcquisition/Security")]
    [ApiController]
    [Authorize]
    public class SecurityController : ControllerBase
    {
        private readonly ISecurityService _securityService;
        private ResponseDto _response;

        public SecurityController(ISecurityService securityService)
        {
            _securityService = securityService;
            _response = new ResponseDto();
        }

        [HttpGet]
        [Route("GetAllSecurities")]
        // GET: SecurityController
        public async Task<ResponseDto> GetAllSecurities()
        {
            try
            {
                IEnumerable<Security> objList = await _securityService.GetAllSecuritysAsync();
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
        [Route("GetSecuritiesbyNameorSymbolAsync/{name}")]
        // GET: SecurityController
        public async Task<ResponseDto> GetSecuritiesbyNameorSymbolAsync(string name)
        {
            try
            {
                IEnumerable<Security> objList = await _securityService.GetFilteredSecurityAsync(name);
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
        [Route("GetSecurityById/{id}")]
        // GET: SecurityController
        public async Task<ResponseDto> GetSecurityById(int id)
        {
            try
            {
                Security objList = await _securityService.GetSecurityByIdAsync(id);
                _response.Result = objList;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        // GET: SecurityController/Details/5
        //public async Task<ActionResult> Details(int id)
        //{
        //    var security = await _securityService.GetSecurityByIdAsync(id);
        //    if (security == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(security);
        //}

        // POST: SecurityController/Create
        [HttpPost("Create")]
        public async Task<ResponseDto> Create([FromBody] SecurityDTO securityDto)
        {
            Security security = new();

            security.Name = securityDto.Name;
            security.Symbol = securityDto.Symbol;
            security.Series = securityDto.Series;
            security.ListingDate = securityDto.ListingDate;
            security.MarketLot = securityDto.MarketLot;
            security.SecurityType = securityDto.SecurityType;
            security.CreatedBy = User.Identity.Name;
            security.ModifiedBy = User.Identity.Name;
            try
            {
                await _securityService.AddSecurityAsync(security);


                _response.Result = security;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        // POST: SecurityController/Edit/5
        [HttpPost("Edit")]
        public async Task<ResponseDto> Edit(SecurityDTO securityDto)
        {
            Security security = new();

            security.Name = securityDto.Name;
            security.ID = securityDto.ID;
            security.Symbol = securityDto.Symbol;
            security.Series = securityDto.Series;
            security.ListingDate = securityDto.ListingDate;
            security.MarketLot = securityDto.MarketLot;
            security.SecurityType = securityDto.SecurityType;
            security.ModifiedBy = User.Identity.Name;
            try
            {                              
                await _securityService.UpdateSecurityAsync(security);

                _response.Result = security;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }


        // POST: SecurityController/Delete/5
        [HttpPost("Delete/{id}")]
        public async Task<ResponseDto> Delete(int id)
        {
            try
            {
                var security = await _securityService.GetSecurityByIdAsync(id);
                if (security != null)
                {
                    await _securityService.DeleteSecurityAsync(id);
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
