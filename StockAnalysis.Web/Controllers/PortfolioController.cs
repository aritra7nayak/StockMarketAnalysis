using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StockAnalysis.Web.Models;
using StockAnalysis.Web.Service.IService;
using System.Collections.Generic;
using System.Security.Claims;

namespace StockAnalysis.Web.Controllers
{
    public class PortfolioController : Controller
    {
        private readonly IPortfolioService _portfolioService;

        public PortfolioController(IPortfolioService portfolioService)
        {
            _portfolioService = portfolioService;
        }

        public async Task<IActionResult> Index()
        {
            List<Portfolio>? list = new();
            ResponseDto? response = new();

            
                response = await _portfolioService.GetUserPortfoliosAsync();
            

            if (response != null && response.IsSuccess)
            {

                list = JsonConvert.DeserializeObject<List<Portfolio>>(Convert.ToString(response.Result));
            }
            else
            {
                TempData["error"] = response?.Message;
            }
            return View(list);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Portfolio model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Gets the user ID from the claims

            if (ModelState.IsValid)
            {
                // Map ViewModel to your Portfolio entity
                Portfolio portfolio = new Portfolio
                {
                    Name = model.Name,
                    Owner = userId,
                    Stocks = model.Stocks.Select(s => new Stock
                    {
                        SecurityId = s.SecurityId,
                        SecurityName = s.SecurityName,
                        Quantity = s.Quantity,
                        BuyPrice = s.BuyPrice,
                        PresentPrice = s.PresentPrice
                    }).ToList()
                };

                try
                {
                    ResponseDto? response = await _portfolioService.AddPortfolioAsync(portfolio);

                    if (response != null && response.IsSuccess)
                    {
                        TempData["success"] = "Portfolio Created Successfully";

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

                return RedirectToAction("Index");
            }

            return View(model);
        }


        public async Task<ActionResult>  Edit(Guid id)
        {
            Portfolio list = new();
            var portfolio = await _portfolioService.GetPortfolioByIdAsync(id);
            if (portfolio != null && portfolio.IsSuccess)
            {

                list = JsonConvert.DeserializeObject<Portfolio>(Convert.ToString(portfolio.Result));
            }
            else
            {
                TempData["error"] = portfolio?.Message;
            }
            return View(list);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Portfolio model)
        {
            Portfolio list = new();

            if (ModelState.IsValid)
            {
                var existingPortfolio = await _portfolioService.GetPortfolioByIdAsync(model.Id);

                if (existingPortfolio != null && existingPortfolio.IsSuccess)
                {

                    list = JsonConvert.DeserializeObject<Portfolio>(Convert.ToString(existingPortfolio.Result));
                }
                else
                {
                    TempData["error"] = existingPortfolio?.Message;
                }

                // Update the portfolio with the new data
                list.Name = model.Name;
                list.Stocks = model.Stocks.Select(s => new Stock
                {
                    SecurityId = s.SecurityId,
                    SecurityName = s.SecurityName,
                    Quantity = s.Quantity,
                    BuyPrice = s.BuyPrice,
                    PresentPrice = s.PresentPrice
                }).ToList();

                try
                {
                    ResponseDto? response = await _portfolioService.UpdatePortfolioAsync(list);

                    if (response != null && response.IsSuccess)
                    {
                        TempData["success"] = "Portfolio Updated Successfully";
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

                return RedirectToAction("Index");
            }

            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            Portfolio portfolio = new();
            var response = await _portfolioService.GetPortfolioByIdAsync(id);

            if (response != null && response.IsSuccess)
            {
                portfolio = JsonConvert.DeserializeObject<Portfolio>(Convert.ToString(response.Result));
            }
            else
            {
                TempData["error"] = response?.Message;
                return RedirectToAction(nameof(Index));
            }

            return View(portfolio);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            try
            {
                var response = await _portfolioService.DeletePortfolioAsync(id);

                if (response != null && response.IsSuccess)
                {
                    TempData["success"] = "Portfolio deleted successfully.";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["error"] = response?.Message;
                }
            }
            catch
            {
                TempData["error"] = "An error occurred while deleting the portfolio.";
            }

            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> GetSecurityAutoComplete(string name)
        {
            ResponseDto? response = new();
            List<SecurityAutoCompleteDto>? list = new();

            response = await _portfolioService.GetSecurityAutoComplete(name);

            if (response != null && response.IsSuccess)
            {

                list = JsonConvert.DeserializeObject<List<SecurityAutoCompleteDto>>(Convert.ToString(response.Result));
            }
            else
            {
                TempData["error"] = response?.Message;
            }
            var autocompleteResults = list.Select(item => new
            {
                label = item.Name,  // Display stock name
                value = item.Name,  // The selected value will be the stock name
                id = item.SecurityID,  // Hidden security ID
                price = item.LatestPrice  // Latest price
            });

            return Ok(autocompleteResults);


        }
    }
}
