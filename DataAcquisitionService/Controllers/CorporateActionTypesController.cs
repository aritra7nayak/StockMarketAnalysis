using Microsoft.AspNetCore.Mvc;

namespace DataAcquisitionService.Controllers
{
    public class CorporateActionTypesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
