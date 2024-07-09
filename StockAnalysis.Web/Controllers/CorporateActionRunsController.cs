using Microsoft.AspNetCore.Mvc;

namespace StockAnalysis.Web.Controllers
{
    public class CorporateActionRunsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
