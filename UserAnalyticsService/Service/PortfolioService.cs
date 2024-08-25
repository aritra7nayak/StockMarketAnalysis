using Microsoft.AspNetCore.Http.HttpResults;
using System.Xml.Linq;
using UserAnalyticsService.DTOs;
using UserAnalyticsService.Models;
using UserAnalyticsService.Repository;
using UserAnalyticsService.Repository.IRepository;

namespace UserAnalyticsService.Service
{
    public class PortfolioService
    {
        private readonly IPortfolioRepository _portfolioRepository;
        private readonly IPriceSyncRepository _priceSyncRepository;


        public PortfolioService(IPortfolioRepository portfolioRepository , IPriceSyncRepository priceSyncRepository)
        {
            _portfolioRepository = portfolioRepository;
            _priceSyncRepository = priceSyncRepository;
        }

        // Retrieve all portfolios
        public async Task<IEnumerable<Portfolio>> GetAllPortfolios()
        {
            return await _portfolioRepository.GetAll();
        }

        // Retrieve a specific portfolio by ID
        public async Task<Portfolio> GetPortfolioById(Guid id, string userId)
        {
            var result = await _portfolioRepository.Find(p => p.Owner == userId && p.Id==id);
            return result.FirstOrDefault();
        }

        // Add a new portfolio
        public async Task AddPortfolio(Portfolio portfolio)
        {
            portfolio.UpdateValues();
            await _portfolioRepository.Add(portfolio);
        }

        // Update an existing portfolio
        public async Task<bool> UpdatePortfolio(Portfolio portfolio)
        {
            var result = await _portfolioRepository.Find(p => p.Owner == portfolio.Owner && p.Id == portfolio.Id);
            if (result != null)
            {
                portfolio.UpdateValues();
                return await _portfolioRepository.Update(portfolio);

            }
            else
            {
                return false;
            }
        }

        // Delete a portfolio by ID
        public async Task<bool> DeletePortfolio(Guid id)
        {
            return await _portfolioRepository.Delete(id);
        }

        // Example of a specific method that might be unique to Portfolio
        public async Task<IEnumerable<Portfolio>> GetPortfoliosByOwner(string name)
        {
            return await _portfolioRepository.Find(p => p.Owner == name);
        }

        public async Task<List<SecurityAutoCompleteDto>> GetSecuritiesAutocompleteAsync(string securityNamePart)
        {
            return await _priceSyncRepository.GetSecuritiesAutocompleteAsync(securityNamePart);
        }
    }
}
