using DataAcquisitionService.Models;
using DataAcquisitionService.Services.IService;
using Microsoft.AspNetCore.Mvc;

namespace DataAcquisitionService.Controllers
{
    public class SecurityController : Controller
    {
        private readonly ISecurityService _securityService;

        public SecurityController(ISecurityService securityService)
        {
            _securityService = securityService;
        }

        // GET: SecurityController
        public async Task<IActionResult> Index(string name, string symbol)
        {
            var customers = await _securityService.GetFilteredSecurityAsync(name, symbol);
            return View(customers);
        }

        // GET: SecurityController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var security = await _securityService.GetSecurityByIdAsync(id);
            if (security == null)
            {
                return NotFound();
            }
            return View(security);
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
            try
            {
                if (ModelState.IsValid)
                {
                    await _securityService.AddSecurityAsync(security);
                    return RedirectToAction(nameof(Index));
                }
                return View(security);
            }
            catch
            {
                return View();
            }
        }

        // GET: SecurityController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var security = await _securityService.GetSecurityByIdAsync(id);
            if (security == null)
            {
                return NotFound();
            }
            return View(security);
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
            var security = await _securityService.GetSecurityByIdAsync(id);
            if (security == null)
            {
                return NotFound();
            }
            return View(security);
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
