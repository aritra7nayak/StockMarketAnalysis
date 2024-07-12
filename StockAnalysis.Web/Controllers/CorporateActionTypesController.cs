using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StockAnalysis.Web.Models;
using StockAnalysis.Web.Service.IService;

namespace StockAnalysis.Web.Controllers
{
    public class CorporateActionTypesController : Controller
    {
        private readonly ICorporateActionTypeService _corporateActionTypeService;

        public CorporateActionTypesController(ICorporateActionTypeService corporateActionTypeService)
        {
            _corporateActionTypeService = corporateActionTypeService;
        }

        // GET: CorporateActionTypeController
        public async Task<IActionResult> Index(string name)
        {
            List<CorporateActionType>? list = new();
            ResponseDto? response = new();

            if (name == null)
            {
                response = await _corporateActionTypeService.GetAllCorporateActionTypesAsync();
            }
            else
            {
                response = await _corporateActionTypeService.GetCorporateActionTypesbyNameorSymbolAsync(name);
            }

            if (response != null && response.IsSuccess)
            {

                list = JsonConvert.DeserializeObject<List<CorporateActionType>>(Convert.ToString(response.Result));
            }
            else
            {
                TempData["error"] = response?.Message;
            }
            return View(list);
        }

        // GET: CorporateActionTypeController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            CorporateActionType list = new();
            var corporateActionType = await _corporateActionTypeService.GetCorporateActionTypeByIdAsync(id);
            if (corporateActionType != null && corporateActionType.IsSuccess)
            {

                list = JsonConvert.DeserializeObject<CorporateActionType>(Convert.ToString(corporateActionType.Result));
            }
            else
            {
                TempData["error"] = corporateActionType?.Message;
            }
            return View(list);
        }

        // GET: CorporateActionTypeController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CorporateActionTypeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CorporateActionType corporateActionType)
        {
            //corporateActionType.ID = 2;
            try
            {
                ResponseDto? response = await _corporateActionTypeService.AddCorporateActionTypeAsync(corporateActionType);

                if (response != null && response.IsSuccess)
                {
                    TempData["success"] = "CorporateActionType Created Successfully";

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

        // GET: CorporateActionTypeController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            CorporateActionType list = new();
            var corporateActionType = await _corporateActionTypeService.GetCorporateActionTypeByIdAsync(id);
            if (corporateActionType != null && corporateActionType.IsSuccess)
            {

                list = JsonConvert.DeserializeObject<CorporateActionType>(Convert.ToString(corporateActionType.Result));
            }
            else
            {
                TempData["error"] = corporateActionType?.Message;
            }
            return View(list);
        }

        // POST: CorporateActionTypeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, CorporateActionType corporateActionType)
        {
            try
            {
                if (id != corporateActionType.ID)
                {
                    return BadRequest();
                }

                if (ModelState.IsValid)
                {
                    await _corporateActionTypeService.UpdateCorporateActionTypeAsync(corporateActionType);
                    return RedirectToAction(nameof(Index));
                }
                return View(corporateActionType);
            }
            catch
            {
                return View();
            }
        }

        // GET: CorporateActionTypeController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            CorporateActionType list = new();
            var corporateActionType = await _corporateActionTypeService.GetCorporateActionTypeByIdAsync(id);
            if (corporateActionType != null && corporateActionType.IsSuccess)
            {

                list = JsonConvert.DeserializeObject<CorporateActionType>(Convert.ToString(corporateActionType.Result));
            }
            else
            {
                TempData["error"] = corporateActionType?.Message;
            }
            return View(list);
        }

        // POST: CorporateActionTypeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                var corporateActionType = await _corporateActionTypeService.GetCorporateActionTypeByIdAsync(id);
                if (corporateActionType != null)
                {
                    await _corporateActionTypeService.DeleteCorporateActionTypeAsync(id);
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
