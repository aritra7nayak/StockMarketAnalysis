using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using UserAnalyticsService.DTOs;
using UserAnalyticsService.Models;
using UserAnalyticsService.Service;

namespace UserAnalyticsService.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BankDepositController : ControllerBase
    {
        private readonly BankDepositService _bankDepositService;
        private ResponseDto _response;


        public BankDepositController(BankDepositService bankDepositService)
        {
            _bankDepositService = bankDepositService;
            _response = new ResponseDto();

        }

        // GET: api/bankDeposit
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BankDeposit>>> GetAllBankDeposits()
        {
            var bankDeposits = await _bankDepositService.GetAllBankDeposits();
            return Ok(bankDeposits);
        }

        // GET: api/bankDeposit/{id}
        [HttpGet]
        [Route("GetBankDepositById/{id}")]

        public async Task<ResponseDto> GetBankDepositById(Guid id)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var bankDeposit = await _bankDepositService.GetBankDepositById(id, userId);
                _response.Result = bankDeposit;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        // POST: api/bankDeposit
        [HttpPost("AddBankDeposit")]
        public async Task<ActionResult> AddBankDeposit([FromBody] BankDeposit model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Gets the user ID from the claims

            if (model == null)
            {
                return BadRequest();
            }
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

            await _bankDepositService.AddBankDeposit(bankDeposit);
            return CreatedAtAction(nameof(GetBankDepositById), new { id = bankDeposit.Id }, bankDeposit);
        }

        // PUT: api/bankDeposit/{id}
        [HttpPost("UpdateBankDeposit")]
        public async Task<ActionResult> UpdateBankDeposit([FromBody] BankDeposit bankDeposit)
        {
            if (bankDeposit == null)
            {
                return BadRequest();
            }

            var updated = await _bankDepositService.UpdateBankDeposit(bankDeposit);
            if (!updated)
            {
                return NotFound();
            }

            return NoContent();
        }

        // DELETE: api/bankDeposit/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBankDeposit(Guid id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var bankDeposit = await _bankDepositService.GetBankDepositById(id, userId);
            var deleted = await _bankDepositService.DeleteBankDeposit(bankDeposit.Id);
            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }

        // GET: api/bankDeposit/owner/{ownerId}
        [HttpGet]
        [Route("GetBankDepositsByOwner")]
        public async Task<ResponseDto> GetBankDepositsByOwner()
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Gets the user ID from the claims
                var bankDeposits = await _bankDepositService.GetBankDepositsByOwner(userId);
                _response.Result = bankDeposits;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;

        }
    }
}
