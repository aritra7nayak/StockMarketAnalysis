using Microsoft.AspNetCore.Mvc;

namespace StockAnalysis.Web.Controllers
{
    public class CorporateActionsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
