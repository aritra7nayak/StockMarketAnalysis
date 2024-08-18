using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StockAnalysis.Web.Models;
using StockAnalysis.Web.Service.IService;

namespace StockAnalysis.Web.Controllers
{
    public class PriceSyncController : Controller
    {
        private readonly IPriceSyncService _priceSyncService;

        public PriceSyncController(IPriceSyncService priceSyncService)
        {
            _priceSyncService = priceSyncService;
        }

        public async Task<IActionResult> Index()
        {
            List<PriceSyncProcessRuns>? list = new();
            ResponseDto? response = new();

            response = await _priceSyncService.GetAllPricesAsync();


            if (response != null && response.IsSuccess)
            {

                list = JsonConvert.DeserializeObject<List<PriceSyncProcessRuns>>(Convert.ToString(response.Result));
            }
            else
            {
                TempData["error"] = response?.Message;
            }
            return View(list);
        }
        public ActionResult Create()
        {
            return View();
        }

        // POST: PriceController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(PriceSyncProcessRuns priceSyncProcessRuns)
        {
            priceSyncProcessRuns.CreatedBy = User.Identity.Name;
            priceSyncProcessRuns.ModifiedBy = User.Identity.Name;
            //price.ID = 2;
            try
            {
                ResponseDto? response = await _priceSyncService.AddPriceAsync(priceSyncProcessRuns);

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
    }
}
