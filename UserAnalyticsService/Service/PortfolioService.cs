using UserAnalyticsService.Models;
using UserAnalyticsService.Repository.IRepository;

namespace UserAnalyticsService.Service
{
    public class PortfolioService
    {
        private readonly IPortfolioRepository _portfolioRepository;

        public PortfolioService(IPortfolioRepository portfolioRepository)
        {
            _portfolioRepository = portfolioRepository;
        }

        // Retrieve all portfolios
        public async Task<IEnumerable<Portfolio>> GetAllPortfolios()
        {
            return await _portfolioRepository.GetAll();
        }

        // Retrieve a specific portfolio by ID
        public async Task<Portfolio> GetPortfolioById(Guid id)
        {
            return await _portfolioRepository.GetById(id);
        }

        // Add a new portfolio
        public async Task AddPortfolio(Portfolio portfolio)
        {
            await _portfolioRepository.Add(portfolio);
        }

        // Update an existing portfolio
        public async Task<bool> UpdatePortfolio(Portfolio portfolio)
        {
            return await _portfolioRepository.Update(portfolio);
        }

        // Delete a portfolio by ID
        public async Task<bool> DeletePortfolio(Guid id)
        {
            return await _portfolioRepository.Delete(id);
        }

        // Example of a specific method that might be unique to Portfolio
        public async Task<IEnumerable<Portfolio>> GetPortfoliosByOwner(Guid ownerId)
        {
            return await _portfolioRepository.Find(p => p.Owner == ownerId);
        }
    }
}
