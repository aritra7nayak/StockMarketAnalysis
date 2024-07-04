using DataAcquisitionService.Dtos;
using DataAcquisitionService.Models;
using DataAcquisitionService.Services.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DataAcquisitionService.Controllers
{
    [Route("api/DataAcquisition/Price")]
    [ApiController]
    [Authorize]
    public class PriceController : ControllerBase
    {
        private readonly IPriceService _priceService;
        private ResponseDto _response;

        public PriceController(IPriceService priceService)
        {
            _priceService = priceService;
            _response = new ResponseDto();
        }

        [HttpGet]
        [Route("GetAllPrices")]
        // GET: PriceController
        public async Task<ResponseDto> GetAllPrices()
        {
            try
            {
                IEnumerable<Price> objList = await _priceService.GetAllPricesAsync();
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
        [Route("GetPricesbyNameorSymbolAsync/{name}")]
        // GET: PriceController
        public async Task<ResponseDto> GetPricesbyNameorSymbolAsync(string name)
        {
            try
            {
                IEnumerable<Price> objList = await _priceService.GetFilteredPriceAsync(name);
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
        [Route("GetPriceById/{id}")]
        // GET: PriceController
        public async Task<ResponseDto> GetPriceById(int id)
        {
            try
            {
                Price objList = await _priceService.GetPriceByIdAsync(id);
                _response.Result = objList;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        // GET: PriceController/Details/5
        //public async Task<ActionResult> Details(int id)
        //{
        //    var price = await _priceService.GetPriceByIdAsync(id);
        //    if (price == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(price);
        //}

        // POST: PriceController/Create
        [HttpPost("Create")]
        public async Task<ResponseDto> Create([FromBody] PriceDTO priceDto)
        {
            Price price = new();

            price.Date = priceDto.Date;
            price.SecurityID = priceDto.SecurityID;
            price.Exchange = priceDto.Exchange;
            price.Open = priceDto.Open;
            price.High = priceDto.High;
            price.Low = priceDto.Low;
            price.Close = priceDto.Close;
            price.LTP = priceDto.LTP;
            price.TradedVolume = priceDto.TradedVolume;
            price.CreatedBy = User.Identity.Name;
            price.ModifiedBy = User.Identity.Name;
            try
            {
                await _priceService.AddPriceAsync(price);


                _response.Result = price;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        // POST: PriceController/Edit/5
        [HttpPost("Edit")]
        public async Task<ResponseDto> Edit(PriceDTO priceDto)
        {
            Price price = new();

            price.Date = priceDto.Date;
            price.ID = (int)priceDto.ID;
            price.SecurityID = priceDto.SecurityID;
            price.Exchange = priceDto.Exchange;
            price.Open = priceDto.Open;
            price.High = priceDto.High;
            price.Low = priceDto.Low;
            price.Close = priceDto.Close;
            price.LTP = priceDto.LTP;
            price.TradedVolume = priceDto.TradedVolume;
            price.ModifiedBy = User.Identity.Name;
            try
            {
                await _priceService.UpdatePriceAsync(price);

                _response.Result = price;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }


        // POST: PriceController/Delete/5
        [HttpPost("Delete/{id}")]
        public async Task<ResponseDto> Delete(int id)
        {
            try
            {
                var price = await _priceService.GetPriceByIdAsync(id);
                if (price != null)
                {
                    await _priceService.DeletePriceAsync(id);
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