using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StockAnalysis.Web.Models;
using StockAnalysis.Web.Service.IService;

namespace StockAnalysis.Web.Controllers
{
    public class PortfolioController : Controller
    {
        private readonly IPortfolioService _portfolioService;

        public PortfolioController(IPortfolioService portfolioService)
        {
            _portfolioService = portfolioService;
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
                    Stocks = model.Stocks.Select(s => new Stock
                    {
                        SecurityId = s.SecurityId,
                        Quantity = s.Quantity,
                        BuyPrice = s.BuyPrice,
                        PresentPrice = s.PresentPrice
                    }).ToList()
                };

                // Save portfolio using service
                //  await _portfolioService.CreatePortfolioAsync(portfolio);

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
