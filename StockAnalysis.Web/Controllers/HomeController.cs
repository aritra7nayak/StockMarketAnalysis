using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StockAnalysis.Web.Models;
using StockAnalysis.Web.Service.IService;
using System.Diagnostics;

namespace StockAnalysis.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBankDepositService _bankDepositService;
        private readonly IPortfolioService _portfolioService;

        public HomeController(ILogger<HomeController> logger, IBankDepositService bankDepositService, IPortfolioService portfolioService)
        {
            _logger = logger;
            _bankDepositService = bankDepositService;
            _portfolioService = portfolioService;
        }

        [HttpGet("api/portfolio/details")]
        public async Task<IActionResult> GetPortfolioDetails()
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
            // Logic to fetch portfolio details, e.g., from a database
            var portfolioDetails = new
            {
                Invested = list.Sum(s=> s.BuyValue),
                Current = list.Sum(s => s.NowValue),
            };
            return Ok(portfolioDetails);
        }

        [HttpGet("api/bank/details")]
        public async Task<IActionResult> GetBankDetails()
        {
            List<BankDeposit>? list = new();
            ResponseDto? response = new();


            response = await _bankDepositService.GetUserBankDepositsAsync();


            if (response != null && response.IsSuccess)
            {

                list = JsonConvert.DeserializeObject<List<BankDeposit>>(Convert.ToString(response.Result));
            }
            else
            {
                TempData["error"] = response?.Message;
            }
            // Logic to fetch bank details, e.g., from a database
            var bankDetails = new
            {
                Invested = list.Sum(s => s.TotalPrincipalValue),
                Maturity = list.Sum(s=>s.TotalMaturityAmount)
            };
            return Ok(bankDetails);
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
