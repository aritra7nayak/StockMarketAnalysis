using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StockAnalysis.Web.Models;
using StockAnalysis.Web.Service.IService;
using System.Collections.Generic;
using System.Reflection;

namespace StockAnalysis.Web.Controllers
{
    public class SecurityController : Controller
    {
        private readonly ISecurityService _securityService;

        public SecurityController(ISecurityService securityService)
        {
            _securityService = securityService;
        }

        // GET: SecurityController
        public async Task<IActionResult> Index(string name)
        {
            List<Security>? list = new();
            ResponseDto? response = new();

            if(name == null)
            {
                response = await _securityService.GetAllSecuritysAsync();
            }
            else
            {
                response = await _securityService.GetSecuritiesbyNameorSymbolAsync(name);
            }
            
            if (response != null && response.IsSuccess)
            {

                list = JsonConvert.DeserializeObject<List<Security>>(Convert.ToString(response.Result));
            }
            else
            {
                TempData["error"] = response?.Message;
            }
            return View(list);
        }

        // GET: SecurityController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            Security list = new();
            var security = await _securityService.GetSecurityByIdAsync(id);
            if (security != null && security.IsSuccess)
            {

                list = JsonConvert.DeserializeObject<Security>(Convert.ToString(security.Result));
            }
            else
            {
                TempData["error"] = security?.Message;
            }
            return View(list);
        }

        // GET: SecurityController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SecurityController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Security security)
        {
            security.CreatedBy = User.Identity.Name;
            security.ModifiedBy = User.Identity.Name;
            //security.ID = 2;
            try
            {
                ResponseDto? response = await _securityService.AddSecurityAsync(security);

                if (response != null && response.IsSuccess)
                {
                    TempData["success"] = "Coupon Created Successfully";

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

        // GET: SecurityController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            Security list = new();
            var security = await _securityService.GetSecurityByIdAsync(id);
            if (security != null && security.IsSuccess)
            {

                list = JsonConvert.DeserializeObject<Security>(Convert.ToString(security.Result));
            }
            else
            {
                TempData["error"] = security?.Message;
            }
            return View(list);
        }

        // POST: SecurityController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Security security)
        {
            security.ModifiedBy = User.Identity.Name;
            try
            {
                if (id != security.ID)
                {
                    return BadRequest();
                }

                if (ModelState.IsValid)
                {
                    await _securityService.UpdateSecurityAsync(security);
                    return RedirectToAction(nameof(Index));
                }
                return View(security);
            }
            catch
            {
                return View();
            }
        }

        // GET: SecurityController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            Security list = new();
            var security = await _securityService.GetSecurityByIdAsync(id);
            if (security != null && security.IsSuccess)
            {

                list = JsonConvert.DeserializeObject<Security>(Convert.ToString(security.Result));
            }
            else
            {
                TempData["error"] = security?.Message;
            }
            return View(list);
        }

        // POST: SecurityController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                var security = await _securityService.GetSecurityByIdAsync(id);
                if (security != null)
                {
                    await _securityService.DeleteSecurityAsync(id);
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
