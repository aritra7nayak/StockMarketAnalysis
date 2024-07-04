using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StockAnalysis.Web.Models;
using StockAnalysis.Web.Service.IService;

namespace StockAnalysis.Web.Controllers
{
    public class PriceController : Controller
    {
        private readonly IPriceService _priceService;

        public PriceController(IPriceService priceService)
        {
            _priceService = priceService;
        }

        // GET: PriceController
        public async Task<IActionResult> Index(string name)
        {
            List<Price>? list = new();
            ResponseDto? response = new();

            if (name == null)
            {
                response = await _priceService.GetAllPricesAsync();
            }
            else
            {
                response = await _priceService.GetSecuritiesbyNameorSymbolAsync(name);
            }

            if (response != null && response.IsSuccess)
            {

                list = JsonConvert.DeserializeObject<List<Price>>(Convert.ToString(response.Result));
            }
            else
            {
                TempData["error"] = response?.Message;
            }
            return View(list);
        }

        // GET: PriceController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            Price list = new();
            var price = await _priceService.GetPriceByIdAsync(id);
            if (price != null && price.IsSuccess)
            {

                list = JsonConvert.DeserializeObject<Price>(Convert.ToString(price.Result));
            }
            else
            {
                TempData["error"] = price?.Message;
            }
            return View(list);
        }

        // GET: PriceController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PriceController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Price price)
        {
            try
            {
                ResponseDto? response = await _priceService.AddPriceAsync(price);

                if (response != null && response.IsSuccess)
                {
                    TempData["success"] = "Price Created Successfully";

                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["error"] = response?.Message;
                }
            }
            catch
            {
                return View();
            }
            return View();
        }

        // GET: PriceController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            Price list = new();
            var price = await _priceService.GetPriceByIdAsync(id);
            if (price != null && price.IsSuccess)
            {

                list = JsonConvert.DeserializeObject<Price>(Convert.ToString(price.Result));
            }
            else
            {
                TempData["error"] = price?.Message;
            }
            return View(list);
        }

        // POST: PriceController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Price price)
        {
            try
            {
                if (id != price.ID)
                {
                    return BadRequest();
                }

                if (ModelState.IsValid)
                {
                    await _priceService.UpdatePriceAsync(price);
                    return RedirectToAction(nameof(Index));
                }
                return View(price);
            }
            catch
            {
                return View();
            }
        }

        // GET: PriceController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            Price list = new();
            var price = await _priceService.GetPriceByIdAsync(id);
            if (price != null && price.IsSuccess)
            {

                list = JsonConvert.DeserializeObject<Price>(Convert.ToString(price.Result));
            }
            else
            {
                TempData["error"] = price?.Message;
            }
            return View(list);
        }

        // POST: PriceController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                var price = await _priceService.GetPriceByIdAsync(id);
                if (price != null)
                {
                    await _priceService.DeletePriceAsync(id);
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}