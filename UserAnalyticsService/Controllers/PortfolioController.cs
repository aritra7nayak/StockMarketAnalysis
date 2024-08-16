using Microsoft.AspNetCore.Mvc;
using UserAnalyticsService.Models;
using UserAnalyticsService.Service;

namespace UserAnalyticsService.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class PortfolioController : ControllerBase
    {
        private readonly PortfolioService _portfolioService;

        public PortfolioController(PortfolioService portfolioService)
        {
            _portfolioService = portfolioService;
        }

        // GET: api/portfolio
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Portfolio>>> GetAllPortfolios()
        {
            var portfolios = await _portfolioService.GetAllPortfolios();
            return Ok(portfolios);
        }

        // GET: api/portfolio/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Portfolio>> GetPortfolioById(Guid id)
        {
            var portfolio = await _portfolioService.GetPortfolioById(id);
            if (portfolio == null)
            {
                return NotFound();
            }
            return Ok(portfolio);
        }

        // POST: api/portfolio
        [HttpPost]
        public async Task<ActionResult> AddPortfolio([FromBody] Portfolio portfolio)
        {
            if (portfolio == null)
            {
                return BadRequest();
            }

            await _portfolioService.AddPortfolio(portfolio);
            return CreatedAtAction(nameof(GetPortfolioById), new { id = portfolio.Id }, portfolio);
        }

        // PUT: api/portfolio/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdatePortfolio(Guid id, [FromBody] Portfolio portfolio)
        {
            if (portfolio == null || portfolio.Id != id)
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
            var deleted = await _portfolioService.DeletePortfolio(id);
            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }

        // GET: api/portfolio/owner/{ownerId}
        [HttpGet("owner/{ownerId}")]
        public async Task<ActionResult<IEnumerable<Portfolio>>> GetPortfoliosByOwner(string ownerId)
        {
            var portfolios = await _portfolioService.GetPortfoliosByOwner(ownerId);
            return Ok(portfolios);
        }
    }
}
