using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StockAnalysis.Web.Models;
using StockAnalysis.Web.Service.IService;

namespace StockAnalysis.Web.Controllers
{
    public class PriceRunsController : Controller
    {

        private readonly IPriceRunService _priceRunService;

        public PriceRunsController(IPriceRunService priceRunService)
        {
            _priceRunService = priceRunService;
        }



        // GET: PriceRun
        public async Task<IActionResult> Index()
        {
            List<PriceRunDto>? list = new();
            ResponseDto? response = new();


            response = await _priceRunService.GetAllPriceRunsAsync();


            if (response != null && response.IsSuccess)
            {

                list = JsonConvert.DeserializeObject<List<PriceRunDto>>(Convert.ToString(response.Result));
            }
            else
            {
                TempData["error"] = response?.Message;
            }
            return View(list);
        }

        // GET: PriceRun/Details/5
        public async Task<IActionResult> Details(int id)
        {
            PriceRunDto list = new();
            var price = await _priceRunService.GetPriceRunByIdAsync(id);
            if (price != null && price.IsSuccess)
            {

                list = JsonConvert.DeserializeObject<PriceRunDto>(Convert.ToString(price.Result));
            }
            else
            {
                TempData["error"] = price?.Message;
            }
            return View(list);
        }

        // GET: PriceRun/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PriceRun/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PriceRunDto priceRunDto, IFormFile File)
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
                        priceRunDto.FileStream = memoryStream.ToArray();
                    }
                    priceRunDto.FilePath = filePath;
                }
                try
                {
                    ResponseDto? response = await _priceRunService.AddPriceRunAsync(priceRunDto);

                    if (response != null && response.IsSuccess)
                    {
                        TempData["success"] = "PriceRun Created Successfully";

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
            return View(priceRunDto);
        }

        // GET: PriceRun/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            PriceRunDto list = new();
            var price = await _priceRunService.GetPriceRunByIdAsync(id);
            if (price != null && price.IsSuccess)
            {

                list = JsonConvert.DeserializeObject<PriceRunDto>(Convert.ToString(price.Result));
            }
            else
            {
                TempData["error"] = price?.Message;
            }
            return View(list);
        }

        // POST: PriceRun/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Date,SourceType,ProcessType,InsertType,ErrorMessage")] PriceRunDto priceRunDto, IFormFile File)
        {
            //if (id != priceRunDto.Id)
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
            //            priceRunDto.FilePath = filePath;
            //        }

            //        _context.Update(priceRunDto);
            //        await _context.SaveChangesAsync();
            //    }
            //    catch (DbUpdateConcurrencyException)
            //    {
            //        if (!PriceRunExists(priceRunDto.Id))
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

        // GET: PriceRun/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            PriceRunDto list = new();
            var price = await _priceRunService.GetPriceRunByIdAsync(id);
            if (price != null && price.IsSuccess)
            {

                list = JsonConvert.DeserializeObject<PriceRunDto>(Convert.ToString(price.Result));
            }
            else
            {
                TempData["error"] = price?.Message;
            }
            return View(list);
        }

        // POST: PriceRun/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var price = await _priceRunService.GetPriceRunByIdAsync(id);
                if (price != null)
                {
                    await _priceRunService.DeletePriceAsync(id);
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
