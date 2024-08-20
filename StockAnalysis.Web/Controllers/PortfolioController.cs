using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StockAnalysis.Web.Models;
using StockAnalysis.Web.Service.IService;
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
            if (ModelState.IsValid)
            {
                // Map ViewModel to your Portfolio entity
                Portfolio portfolio = new Portfolio
                {
                    Name = model.Name,
                    Owner = User.Identity.Name,
                    Stocks = model.Stocks.Select(s => new Stock
                    {
                        SecurityId = s.SecurityId,
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
