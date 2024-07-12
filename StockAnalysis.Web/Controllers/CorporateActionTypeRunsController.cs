using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StockAnalysis.Web.Models;
using StockAnalysis.Web.Service.IService;

namespace StockAnalysis.Web.Controllers
{
    public class CorporateActionTypeRunsController : Controller
    {

        private readonly ICorporateActionTypeRunService _corporateActionTypeRunService;

        public CorporateActionTypeRunsController(ICorporateActionTypeRunService corporateActionTypeRunService)
        {
            _corporateActionTypeRunService = corporateActionTypeRunService;
        }



        // GET: CorporateActionTypeRun
        public async Task<IActionResult> Index()
        {
            List<CorporateActionTypeRunDto>? list = new();
            ResponseDto? response = new();


            response = await _corporateActionTypeRunService.GetAllCorporateActionTypeRunsAsync();


            if (response != null && response.IsSuccess)
            {

                list = JsonConvert.DeserializeObject<List<CorporateActionTypeRunDto>>(Convert.ToString(response.Result));
            }
            else
            {
                TempData["error"] = response?.Message;
            }
            return View(list);
        }

        // GET: CorporateActionTypeRun/Details/5
        public async Task<IActionResult> Details(int id)
        {
            CorporateActionTypeRunDto list = new();
            var corporateActionType = await _corporateActionTypeRunService.GetCorporateActionTypeRunByIdAsync(id);
            if (corporateActionType != null && corporateActionType.IsSuccess)
            {

                list = JsonConvert.DeserializeObject<CorporateActionTypeRunDto>(Convert.ToString(corporateActionType.Result));
            }
            else
            {
                TempData["error"] = corporateActionType?.Message;
            }
            return View(list);
        }

        // GET: CorporateActionTypeRun/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CorporateActionTypeRun/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CorporateActionTypeRunDto corporateActionTypeRunDto, IFormFile File)
        {
            if (ModelState.IsValid)
            {
                if (File != null && File.Length > 0)
                {
                    var filePath = Path.Combine("wwwroot/temp/", Guid.NewGuid().ToString() + Path.GetExtension(File.FileName));
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await File.CopyToAsync(fileStream);

                    }
                    using (var memoryStream = new MemoryStream())
                    {
                        await File.CopyToAsync(memoryStream);
                        corporateActionTypeRunDto.FileStream = memoryStream.ToArray();
                    }
                    corporateActionTypeRunDto.FilePath = filePath;
                }
                try
                {
                    ResponseDto? response = await _corporateActionTypeRunService.AddCorporateActionTypeRunAsync(corporateActionTypeRunDto);

                    if (response != null && response.IsSuccess)
                    {
                        TempData["success"] = "CorporateActionTypeRun Created Successfully";

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
            return View(corporateActionTypeRunDto);
        }

        // GET: CorporateActionTypeRun/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            CorporateActionTypeRunDto list = new();
            var corporateActionType = await _corporateActionTypeRunService.GetCorporateActionTypeRunByIdAsync(id);
            if (corporateActionType != null && corporateActionType.IsSuccess)
            {

                list = JsonConvert.DeserializeObject<CorporateActionTypeRunDto>(Convert.ToString(corporateActionType.Result));
            }
            else
            {
                TempData["error"] = corporateActionType?.Message;
            }
            return View(list);
        }

        // POST: CorporateActionTypeRun/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Date,SourceType,ProcessType,InsertType,ErrorMessage")] CorporateActionTypeRunDto corporateActionTypeRunDto, IFormFile File)
        {
            //if (id != corporateActionTypeRunDto.Id)
            //{
            //    return NotFound();
            //}

            //if (ModelState.IsValid)
            //{
            //    try
            //    {
            //        if (File != null && File.Length > 0)
            //        {
            //            var filePath = Path.Combine("wwwroot/files", Guid.NewGuid().ToString() + Path.GetExtension(File.FileName));
            //            using (var fileStream = new FileStream(filePath, FileMode.Create))
            //            {
            //                await File.CopyToAsync(fileStream);
            //            }
            //            corporateActionTypeRunDto.FilePath = filePath;
            //        }

            //        _context.Update(corporateActionTypeRunDto);
            //        await _context.SaveChangesAsync();
            //    }
            //    catch (DbUpdateConcurrencyException)
            //    {
            //        if (!CorporateActionTypeRunExists(corporateActionTypeRunDto.Id))
            //        {
            //            return NotFound();
            //        }
            //        else
            //        {
            //            throw;
            //        }
            //    }
            //    return RedirectToAction(nameof(Index));
            //}
            return View();
        }

        // GET: CorporateActionTypeRun/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            CorporateActionTypeRunDto list = new();
            var corporateActionType = await _corporateActionTypeRunService.GetCorporateActionTypeRunByIdAsync(id);
            if (corporateActionType != null && corporateActionType.IsSuccess)
            {

                list = JsonConvert.DeserializeObject<CorporateActionTypeRunDto>(Convert.ToString(corporateActionType.Result));
            }
            else
            {
                TempData["error"] = corporateActionType?.Message;
            }
            return View(list);
        }

        // POST: CorporateActionTypeRun/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var corporateActionType = await _corporateActionTypeRunService.GetCorporateActionTypeRunByIdAsync(id);
                if (corporateActionType != null)
                {
                    await _corporateActionTypeRunService.DeleteCorporateActionTypeAsync(id);
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