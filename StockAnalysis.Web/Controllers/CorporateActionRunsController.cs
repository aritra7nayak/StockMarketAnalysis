﻿using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StockAnalysis.Web.Models;
using StockAnalysis.Web.Service.IService;

namespace StockAnalysis.Web.Controllers
{
    public class CorporateActionRunsController : Controller
    {

        private readonly ICorporateActionRunService _corporateActionRunService;

        public CorporateActionRunsController(ICorporateActionRunService corporateActionRunService)
        {
            _corporateActionRunService = corporateActionRunService;
        }



        // GET: CorporateActionRun
        public async Task<IActionResult> Index()
        {
            List<CorporateActionRunDto>? list = new();
            ResponseDto? response = new();


            response = await _corporateActionRunService.GetAllCorporateActionRunsAsync();


            if (response != null && response.IsSuccess)
            {

                list = JsonConvert.DeserializeObject<List<CorporateActionRunDto>>(Convert.ToString(response.Result));
            }
            else
            {
                TempData["error"] = response?.Message;
            }
            return View(list);
        }

        // GET: CorporateActionRun/Details/5
        public async Task<IActionResult> Details(int id)
        {
            CorporateActionRunDto list = new();
            var corporateAction = await _corporateActionRunService.GetCorporateActionRunByIdAsync(id);
            if (corporateAction != null && corporateAction.IsSuccess)
            {

                list = JsonConvert.DeserializeObject<CorporateActionRunDto>(Convert.ToString(corporateAction.Result));
            }
            else
            {
                TempData["error"] = corporateAction?.Message;
            }
            return View(list);
        }

        // GET: CorporateActionRun/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CorporateActionRun/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CorporateActionRunDto corporateActionRunDto, IFormFile File)
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
                        corporateActionRunDto.FileStream = memoryStream.ToArray();
                    }
                    corporateActionRunDto.FilePath = filePath;
                }
                try
                {
                    ResponseDto? response = await _corporateActionRunService.AddCorporateActionRunAsync(corporateActionRunDto);

                    if (response != null && response.IsSuccess)
                    {
                        TempData["success"] = "CorporateActionRun Created Successfully";

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
            return View(corporateActionRunDto);
        }

        // GET: CorporateActionRun/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            CorporateActionRunDto list = new();
            var corporateAction = await _corporateActionRunService.GetCorporateActionRunByIdAsync(id);
            if (corporateAction != null && corporateAction.IsSuccess)
            {

                list = JsonConvert.DeserializeObject<CorporateActionRunDto>(Convert.ToString(corporateAction.Result));
            }
            else
            {
                TempData["error"] = corporateAction?.Message;
            }
            return View(list);
        }

        // POST: CorporateActionRun/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Date,SourceType,ProcessType,InsertType,ErrorMessage")] CorporateActionRunDto corporateActionRunDto, IFormFile File)
        {
            //if (id != corporateActionRunDto.Id)
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
            //            corporateActionRunDto.FilePath = filePath;
            //        }

            //        _context.Update(corporateActionRunDto);
            //        await _context.SaveChangesAsync();
            //    }
            //    catch (DbUpdateConcurrencyException)
            //    {
            //        if (!CorporateActionRunExists(corporateActionRunDto.Id))
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

        // GET: CorporateActionRun/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            CorporateActionRunDto list = new();
            var corporateAction = await _corporateActionRunService.GetCorporateActionRunByIdAsync(id);
            if (corporateAction != null && corporateAction.IsSuccess)
            {

                list = JsonConvert.DeserializeObject<CorporateActionRunDto>(Convert.ToString(corporateAction.Result));
            }
            else
            {
                TempData["error"] = corporateAction?.Message;
            }
            return View(list);
        }

        // POST: CorporateActionRun/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var corporateAction = await _corporateActionRunService.GetCorporateActionRunByIdAsync(id);
                if (corporateAction != null)
                {
                    await _corporateActionRunService.DeleteCorporateActionAsync(id);
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
