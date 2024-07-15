using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StockAnalysis.Web.Models;
using StockAnalysis.Web.Service.IService;

namespace StockAnalysis.Web.Controllers
{
    public class CorporateActionsController : Controller
    {
        private readonly ICorporateActionService _corporateActionService;

        public CorporateActionsController(ICorporateActionService corporateActionService)
        {
            _corporateActionService = corporateActionService;
        }

        // GET: CorporateActionController
        public async Task<IActionResult> Index(string name)
        {
            List<CorporateAction>? list = new();
            ResponseDto? response = new();

            if (name == null)
            {
                response = await _corporateActionService.GetAllCorporateActionsAsync();
            }
            else
            {
                response = await _corporateActionService.GetSecuritiesbyNameorSymbolAsync(name);
            }

            if (response != null && response.IsSuccess)
            {

                list = JsonConvert.DeserializeObject<List<CorporateAction>>(Convert.ToString(response.Result));
            }
            else
            {
                TempData["error"] = response?.Message;
            }
            return View(list);
        }

        // GET: CorporateActionController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            CorporateAction list = new();
            var corporateAction = await _corporateActionService.GetCorporateActionByIdAsync(id);
            if (corporateAction != null && corporateAction.IsSuccess)
            {

                list = JsonConvert.DeserializeObject<CorporateAction>(Convert.ToString(corporateAction.Result));
            }
            else
            {
                TempData["error"] = corporateAction?.Message;
            }
            return View(list);
        }

        // GET: CorporateActionController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CorporateActionController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CorporateAction corporateAction)
        {
            //corporateAction.ID = 2;
            try
            {
                ResponseDto? response = await _corporateActionService.AddCorporateActionAsync(corporateAction);

                if (response != null && response.IsSuccess)
                {
                    TempData["success"] = "CorporateAction Created Successfully";

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

        // GET: CorporateActionController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            CorporateAction list = new();
            var corporateAction = await _corporateActionService.GetCorporateActionByIdAsync(id);
            if (corporateAction != null && corporateAction.IsSuccess)
            {

                list = JsonConvert.DeserializeObject<CorporateAction>(Convert.ToString(corporateAction.Result));
            }
            else
            {
                TempData["error"] = corporateAction?.Message;
            }
            return View(list);
        }

        // POST: CorporateActionController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, CorporateAction corporateAction)
        {
            try
            {
                if (id != corporateAction.ID)
                {
                    return BadRequest();
                }

                if (ModelState.IsValid)
                {
                    await _corporateActionService.UpdateCorporateActionAsync(corporateAction);
                    return RedirectToAction(nameof(Index));
                }
                return View(corporateAction);
            }
            catch
            {
                return View();
            }
        }

        // GET: CorporateActionController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            CorporateAction list = new();
            var corporateAction = await _corporateActionService.GetCorporateActionByIdAsync(id);
            if (corporateAction != null && corporateAction.IsSuccess)
            {

                list = JsonConvert.DeserializeObject<CorporateAction>(Convert.ToString(corporateAction.Result));
            }
            else
            {
                TempData["error"] = corporateAction?.Message;
            }
            return View(list);
        }

        // POST: CorporateActionController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                var corporateAction = await _corporateActionService.GetCorporateActionByIdAsync(id);
                if (corporateAction != null)
                {
                    await _corporateActionService.DeleteCorporateActionAsync(id);
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