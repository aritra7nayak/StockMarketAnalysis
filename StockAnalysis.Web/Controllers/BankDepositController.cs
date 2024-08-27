using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StockAnalysis.Web.Models;
using StockAnalysis.Web.Service.IService;
using System.Security.Claims;

namespace StockAnalysis.Web.Controllers
{
    public class BankDepositController : Controller
    {
        private readonly IBankDepositService _bankDepositService;

        public BankDepositController(IBankDepositService bankDepositService)
        {
            _bankDepositService = bankDepositService;
        }

        public async Task<IActionResult> Index()
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
            return View(list);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(BankDeposit model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Gets the user ID from the claims

            if (ModelState.IsValid)
            {
                // Map ViewModel to your BankDeposit entity
                BankDeposit bankDeposit = new BankDeposit
                {
                    Name = model.Name,
                    Owner = userId,
                    InvestmentDetails = model.InvestmentDetails.Select(i => new InvestmentDetail
                    {
                        FDId = i.FDId,
                        BankName = i.BankName,
                        AccountNumber = i.AccountNumber,
                        PrincipalAmount = i.PrincipalAmount,
                        InterestRate = i.InterestRate,
                        Duration = i.Duration,
                        DurationType = i.DurationType,
                        FDType = i.FDType,
                        StartDate = i.StartDate
                    }).ToList()
                };

                try
                {
                    ResponseDto? response = await _bankDepositService.AddBankDepositAsync(bankDeposit);

                    if (response != null && response.IsSuccess)
                    {
                        TempData["success"] = "BankDeposit Created Successfully";

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


        public async Task<ActionResult> Edit(Guid id)
        {
            BankDeposit list = new();
            var bankDeposit = await _bankDepositService.GetBankDepositByIdAsync(id);
            if (bankDeposit != null && bankDeposit.IsSuccess)
            {

                list = JsonConvert.DeserializeObject<BankDeposit>(Convert.ToString(bankDeposit.Result));
            }
            else
            {
                TempData["error"] = bankDeposit?.Message;
            }
            return View(list);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(BankDeposit model)
        {
            BankDeposit list = new();

            if (ModelState.IsValid)
            {
                var existingBankDeposit = await _bankDepositService.GetBankDepositByIdAsync(model.Id);

                if (existingBankDeposit != null && existingBankDeposit.IsSuccess)
                {

                    list = JsonConvert.DeserializeObject<BankDeposit>(Convert.ToString(existingBankDeposit.Result));
                }
                else
                {
                    TempData["error"] = existingBankDeposit?.Message;
                }

                // Update the bankDeposit with the new data
                list.Name = model.Name;
                list.InvestmentDetails = model.InvestmentDetails.Select(i => new InvestmentDetail
                {
                    FDId = i.FDId,
                    BankName = i.BankName,
                    AccountNumber = i.AccountNumber,
                    PrincipalAmount = i.PrincipalAmount,
                    InterestRate = i.InterestRate,
                    Duration = i.Duration,
                    DurationType = i.DurationType,
                    FDType = i.FDType,
                    StartDate = i.StartDate
                }).ToList();

                try
                {
                    ResponseDto? response = await _bankDepositService.UpdateBankDepositAsync(list);

                    if (response != null && response.IsSuccess)
                    {
                        TempData["success"] = "BankDeposit Updated Successfully";
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
            BankDeposit bankDeposit = new();
            var response = await _bankDepositService.GetBankDepositByIdAsync(id);

            if (response != null && response.IsSuccess)
            {
                bankDeposit = JsonConvert.DeserializeObject<BankDeposit>(Convert.ToString(response.Result));
            }
            else
            {
                TempData["error"] = response?.Message;
                return RedirectToAction(nameof(Index));
            }

            return View(bankDeposit);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            try
            {
                var response = await _bankDepositService.DeleteBankDepositAsync(id);

                if (response != null && response.IsSuccess)
                {
                    TempData["success"] = "BankDeposit deleted successfully.";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["error"] = response?.Message;
                }
            }
            catch
            {
                TempData["error"] = "An error occurred while deleting the bankDeposit.";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
