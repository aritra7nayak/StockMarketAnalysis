using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DataAcquisitionService.Controllers
{
    public class CorporateActionsController : Controller
    {
        // GET: CorporateActionsController
        public ActionResult Index()
        {
            return View();
        }

        // GET: CorporateActionsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CorporateActionsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CorporateActionsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CorporateActionsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CorporateActionsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CorporateActionsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CorporateActionsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
