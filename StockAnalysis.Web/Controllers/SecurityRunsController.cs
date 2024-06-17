using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StockAnalysis.Web.Models;
using StockAnalysis.Web.Service.IService;
using System.Xml.Linq;

namespace StockAnalysis.Web.Controllers
{
    public class SecurityRunsController : Controller
    {

        private readonly ISecurityRunService _securityRunService;

        public SecurityRunsController(ISecurityRunService securityRunService)
        {
            _securityRunService = securityRunService;
        }



        // GET: SecurityRun
        public async Task<IActionResult> Index()
        {
            List<SecurityRunDto>? list = new();
            ResponseDto? response = new();

            
           response = await _securityRunService.GetAllSecurityRunsAsync();
           

            if (response != null && response.IsSuccess)
            {

                list = JsonConvert.DeserializeObject<List<SecurityRunDto>>(Convert.ToString(response.Result));
            }
            else
            {
                TempData["error"] = response?.Message;
            }
            return View(list);
        }

        // GET: SecurityRun/Details/5
        public async Task<IActionResult> Details(int id)
        {
            SecurityRunDto list = new();
            var security = await _securityRunService.GetSecurityRunByIdAsync(id);
            if (security != null && security.IsSuccess)
            {

                list = JsonConvert.DeserializeObject<SecurityRunDto>(Convert.ToString(security.Result));
            }
            else
            {
                TempData["error"] = security?.Message;
            }
            return View(list);
        }

        // GET: SecurityRun/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SecurityRun/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SecurityRunDto securityRunDto, IFormFile File)
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
                        securityRunDto.FileStream = memoryStream.ToArray();
                    }
                    securityRunDto.FilePath = filePath;
                }
                try
                {
                    ResponseDto? response = await _securityRunService.AddSecurityRunAsync(securityRunDto);

                    if (response != null && response.IsSuccess)
                    {
                        TempData["success"] = "SecurityRun Created Successfully";

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
            return View(securityRunDto);
        }

        // GET: SecurityRun/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            SecurityRunDto list = new();
            var security = await _securityRunService.GetSecurityRunByIdAsync(id);
            if (security != null && security.IsSuccess)
            {

                list = JsonConvert.DeserializeObject<SecurityRunDto>(Convert.ToString(security.Result));
            }
            else
            {
                TempData["error"] = security?.Message;
            }
            return View(list);
        }

        // POST: SecurityRun/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Date,SourceType,ProcessType,InsertType,ErrorMessage")] SecurityRunDto securityRunDto, IFormFile File)
        {
            //if (id != securityRunDto.Id)
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
            //            securityRunDto.FilePath = filePath;
            //        }

            //        _context.Update(securityRunDto);
            //        await _context.SaveChangesAsync();
            //    }
            //    catch (DbUpdateConcurrencyException)
            //    {
            //        if (!SecurityRunExists(securityRunDto.Id))
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

        // GET: SecurityRun/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            SecurityRunDto list = new();
            var security = await _securityRunService.GetSecurityRunByIdAsync(id);
            if (security != null && security.IsSuccess)
            {

                list = JsonConvert.DeserializeObject<SecurityRunDto>(Convert.ToString(security.Result));
            }
            else
            {
                TempData["error"] = security?.Message;
            }
            return View(list);
        }

        // POST: SecurityRun/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var security = await _securityRunService.GetSecurityRunByIdAsync(id);
                if (security != null)
                {
                    await _securityRunService.DeleteSecurityAsync(id);
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
