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
    public class PortfolioController : ControllerBase
    {
        private readonly PortfolioService _portfolioService;
        private ResponseDto _response;


        public PortfolioController(PortfolioService portfolioService)
        {
            _portfolioService = portfolioService;
            _response = new ResponseDto();

        }

        // GET: api/portfolio
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Portfolio>>> GetAllPortfolios()
        {
            var portfolios = await _portfolioService.GetAllPortfolios();
            return Ok(portfolios);
        }

        // GET: api/portfolio/{id}
        [HttpGet]
        [Route("GetPortfolioById/{id}")]

        public async Task<ResponseDto> GetPortfolioById(Guid id)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var portfolio = await _portfolioService.GetPortfolioById(id, userId);
                _response.Result = portfolio;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        // POST: api/portfolio
        [HttpPost("AddPortfolio")]
        public async Task<ActionResult> AddPortfolio([FromBody] Portfolio model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Gets the user ID from the claims

            if (model == null)
            {
                return BadRequest();
            }
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

            await _portfolioService.AddPortfolio(portfolio);
            return CreatedAtAction(nameof(GetPortfolioById), new { id = portfolio.Id }, portfolio);
        }

        // PUT: api/portfolio/{id}
        [HttpPost("UpdatePortfolio")]
        public async Task<ActionResult> UpdatePortfolio([FromBody] Portfolio portfolio)
        {
            if (portfolio == null)
            {
                return BadRequest();
            }

            var updated = await _portfolioService.UpdatePortfolio(portfolio);
            if (!updated)
            {
                return NotFound();
            }

            return NoContent();
        }

        // DELETE: api/portfolio/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePortfolio(Guid id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var portfolio = await _portfolioService.GetPortfolioById(id, userId);
            var deleted = await _portfolioService.DeletePortfolio(portfolio.Id);
            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }

        // GET: api/portfolio/owner/{ownerId}
        [HttpGet]
        [Route("GetPortfoliosByOwner")]
        public async Task<ResponseDto> GetPortfoliosByOwner()
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Gets the user ID from the claims
                var portfolios = await _portfolioService.GetPortfoliosByOwner(userId);
                _response.Result = portfolios;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;

        }


        [HttpGet]
        [Route("GetSecurityAutoComplete/{name}")]
        // GET: SecurityController
        public async Task<ResponseDto> GetSecurityAutoComplete(string name)
        {
            try
            {
                IEnumerable<SecurityAutoCompleteDto> objList = await _portfolioService.GetSecuritiesAutocompleteAsync(name);
           _response.Result = objList;
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
