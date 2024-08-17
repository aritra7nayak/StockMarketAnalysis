using Microsoft.AspNetCore.Mvc;
using StockAnalysis.Web.Models;
using StockAnalysis.Web.Service.IService;
using Newtonsoft.Json;


namespace StockAnalysis.Web.Controllers
{
    public class SecuritySyncController : Controller
    {
        private readonly ISecuritySyncService _securitySyncService;

        public SecuritySyncController(ISecuritySyncService securitySyncService)
        {
            _securitySyncService = securitySyncService;
        }

        public async Task<IActionResult> Index()
        {
            List<SecuritySyncProcessRuns>? list = new();
            ResponseDto? response = new();

                response = await _securitySyncService.GetAllSecuritysAsync();
            

            if (response != null && response.IsSuccess)
            {

                list = JsonConvert.DeserializeObject<List<SecuritySyncProcessRuns>>(Convert.ToString(response.Result));
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

        // POST: SecurityController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(SecuritySyncProcessRuns securitySyncProcessRuns)
        {
            securitySyncProcessRuns.CreatedBy = User.Identity.Name;
            securitySyncProcessRuns.ModifiedBy = User.Identity.Name;
            //security.ID = 2;
            try
            {
                ResponseDto? response = await _securitySyncService.AddSecurityAsync(securitySyncProcessRuns);

                if (response != null && response.IsSuccess)
                {
                    TempData["success"] = "Security Created Successfully";

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
